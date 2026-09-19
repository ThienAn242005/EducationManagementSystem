(function ($) {
    var _gradeService = abp.services.app.grade;
    var _classService = abp.services.app.class;
    var _subjectService = abp.services.app.subject;
    var _semesterService = abp.services.app.semester;

    var isCurrentLocked = false;

    function initFilters() {
        _semesterService.getAll({ maxResultCount: 100 }).done(function (res) {
            $('#SemesterFilter').html('<option value="">-- Chọn Học kỳ --</option>');
            res.items.forEach(i => $('#SemesterFilter').append(new Option(i.name, i.id)));
        });
        _classService.getAll({ maxResultCount: 100 }).done(function (res) {
            $('#ClassFilter').html('<option value="">-- Chọn Lớp --</option>');
            res.items.forEach(i => $('#ClassFilter').append(new Option(i.className, i.id)));
        });
        _subjectService.getAll({ maxResultCount: 100 }).done(function (res) {
            $('#SubjectFilter').html('<option value="">-- Chọn Môn --</option>');
            res.items.forEach(i => $('#SubjectFilter').append(new Option(i.name, i.id)));
        });
    }

    initFilters();

    // Bắt sự kiện chọn đủ 3 dropdown
    $('#SemesterFilter, #ClassFilter, #SubjectFilter').change(function () {
        var semId = $('#SemesterFilter').val();
        var clsId = $('#ClassFilter').val();
        var subId = $('#SubjectFilter').val();

        if (semId && clsId && subId) {
            loadGradeBook(clsId, subId, semId);
        } else {
            $('#GradeTableBody').html('<tr><td colspan="9" class="text-muted">Vui lòng chọn đầy đủ Học kỳ, Lớp và Môn để tải sổ điểm.</td></tr>');
            $('#btnSaveGrade, #btnLockGrade').hide();
        }
    });

    function loadGradeBook(classId, subjectId, semesterId) {
        abp.ui.setBusy('#GradeGridTable');
        _gradeService.getGradeBook({
            classId: parseInt(classId),
            subjectId: parseInt(subjectId),
            semesterId: parseInt(semesterId)
        }).done(function (result) {
            renderTable(result);
            $('#btnSaveGrade, #btnLockGrade').show();
        }).always(function () {
            abp.ui.clearBusy('#GradeGridTable');
        });
    }

    function renderTable(data) {
        var $tbody = $('#GradeTableBody').empty();
        if (data.length === 0) {
            $tbody.html('<tr><td colspan="9" class="text-muted">Lớp học này chưa có học sinh nào.</td></tr>');
            return;
        }

        data.forEach(function (row) {
            var tr = `
                <tr data-student-id="${row.studentId}">
                    <td>${row.studentCode}</td>
                    <td class="text-left font-weight-bold">${row.fullName}</td>
                    <td><input type="number" step="0.1" min="0" max="10" class="form-control form-control-sm score-input" data-col="tx1" value="${row.scoreTX1 ?? ''}"></td>
                    <td><input type="number" step="0.1" min="0" max="10" class="form-control form-control-sm score-input" data-col="tx2" value="${row.scoreTX2 ?? ''}"></td>
                    <td><input type="number" step="0.1" min="0" max="10" class="form-control form-control-sm score-input" data-col="tx3" value="${row.scoreTX3 ?? ''}"></td>
                    <td><input type="number" step="0.1" min="0" max="10" class="form-control form-control-sm score-input" data-col="gk" value="${row.scoreGK ?? ''}"></td>
                    <td><input type="number" step="0.1" min="0" max="10" class="form-control form-control-sm score-input" data-col="ck" value="${row.scoreCK ?? ''}"></td>
                    <td class="avg-score font-weight-bold text-primary">${row.averageScore ?? '-'}</td>
                    <td class="perf-text font-weight-bold">${row.performanceDisplay ?? '-'}</td>
                </tr>
            `;
            $tbody.append(tr);
        });
    }

    // Tự động tính nhẩm ĐTB và xếp loại trên trình duyệt khi người dùng gõ điểm
    $(document).on('input', '.score-input', function () {
        var $row = $(this).closest('tr');
        var tx1 = parseFloat($row.find('[data-col="tx1"]').val());
        var tx2 = parseFloat($row.find('[data-col="tx2"]').val());
        var tx3 = parseFloat($row.find('[data-col="tx3"]').val());
        var gk = parseFloat($row.find('[data-col="gk"]').val());
        var ck = parseFloat($row.find('[data-col="ck"]').val());

        var total = 0, weight = 0;
        if (!isNaN(tx1)) { total += tx1; weight += 1; }
        if (!isNaN(tx2)) { total += tx2; weight += 1; }
        if (!isNaN(tx3)) { total += tx3; weight += 1; }
        if (!isNaN(gk)) { total += gk * 2; weight += 2; }
        if (!isNaN(ck)) { total += ck * 3; weight += 3; }

        if (weight > 0) {
            var avg = (total / weight).toFixed(1);
            $row.find('.avg-score').text(avg);

            var rank = "Yếu";
            if (avg >= 8.0) rank = "Giỏi";
            else if (avg >= 6.5) rank = "Khá";
            else if (avg >= 5.0) rank = "Trung bình";
            $row.find('.perf-text').text(rank);
        } else {
            $row.find('.avg-score').text('-'); $row.find('.perf-text').text('-');
        }
    });

    // Thao tác lưu điểm cả lớp
    $('#btnSaveGrade').click(function () {
        var records = [];
        $('#GradeTableBody tr').each(function () {
            var $row = $(this);
            var studentId = $row.data('student-id');
            if (studentId) {
                records.push({
                    studentId: studentId,
                    scoreTX1: $row.find('[data-col="tx1"]').val() || null,
                    scoreTX2: $row.find('[data-col="tx2"]').val() || null,
                    scoreTX3: $row.find('[data-col="tx3"]').val() || null,
                    scoreGK: $row.find('[data-col="gk"]').val() || null,
                    scoreCK: $row.find('[data-col="ck"]').val() || null
                });
            }
        });

        var payload = {
            classId: parseInt($('#ClassFilter').val()),
            subjectId: parseInt($('#SubjectFilter').val()),
            semesterId: parseInt($('#SemesterFilter').val()),
            gradeRecords: records
        };

        abp.ui.setBusy('#GradeGridTable');
        _gradeService.saveGrades(payload).done(function () {
            abp.notify.success('Lưu bảng điểm thành công!');
            loadGradeBook(payload.classId, payload.subjectId, payload.semesterId);
        }).always(function () {
            abp.ui.clearBusy('#GradeGridTable');
        });
    });

    // Thao tác Khóa / Mở sổ
    $('#btnLockGrade').click(function () {
        var classId = parseInt($('#ClassFilter').val());
        var subjectId = parseInt($('#SubjectFilter').val());
        var semesterId = parseInt($('#SemesterFilter').val());

        abp.message.confirm('Bạn có muốn đổi trạng thái khóa sổ cho môn học tại lớp này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _gradeService.setLockStatus({
                    classId: classId,
                    subjectId: subjectId,
                    semesterId: semesterId,
                    isLocked: !isCurrentLocked
                }).done(function () {
                    isCurrentLocked = !isCurrentLocked;
                    $('#lockStatusText').text(isCurrentLocked ? 'Mở khóa sổ' : 'Khóa sổ');
                    abp.notify.success('Đã cập nhật trạng thái khóa!');
                });
            }
        });
    });
})(jQuery);