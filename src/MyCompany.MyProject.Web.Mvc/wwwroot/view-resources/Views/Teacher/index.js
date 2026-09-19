(function ($) {
    var _teacherService = abp.services.app.teacher;
    var _subjectService = abp.services.app.subject;
    var _userService = abp.services.app.user;

    var _$table = $('#TeacherTable');
    var _$form = $('#TeacherCreateForm');
    $('#btnCreateTeacher').click(function () {
        $('#TeacherCreateModal').modal('show');
    });
    $('#btnCloseTeacherModal, #btnCancelTeacherModal').click(function () {
        $('#TeacherCreateModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],
        listAction: { ajaxFunction: _teacherService.getAll },
        columnDefs: [
            { targets: 0, data: 'teacherCode' },
            { targets: 1, data: 'fullName' },
            { targets: 2, data: 'phoneNumber', defaultContent: '-' },
            { targets: 3, data: 'mainSubjectName', defaultContent: '-' },
            {
                targets: 4,
                data: 'isPrincipal',
                render: function (data) {
                    return data ? '<span class="badge badge-danger">Hiệu trưởng</span>' : '<span class="badge badge-info">Giáo viên</span>';
                }
            },
            {
                targets: 5,
                data: null,
                orderable: false,
                render: function (data, type, row) {
                    return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.id}"><i class="fa fa-trash"></i> Xóa</button>`;
                }
            }
        ]
    });

    // Nạp danh sách Users và Môn học khi mở modal
    $('#TeacherCreateModal').on('show.bs.modal', function () {
        _userService.getAll({ maxResultCount: 1000 }).done(function (res) {
            var $userSelect = $('#TeacherUserSelect').empty();
            $userSelect.append(new Option('-- Chọn tài khoản --', ''));
            res.items.forEach(u => $userSelect.append(new Option(`${u.fullName} (${u.userName})`, u.id)));
        });

        _subjectService.getAll({ maxResultCount: 1000 }).done(function (res) {
            var $subSelect = $('#TeacherSubjectSelect').empty();
            $subSelect.append(new Option('-- Không chọn --', ''));
            res.items.forEach(s => $subSelect.append(new Option(s.subjectName, s.id)));
        });
    });

    _$form.submit(function (e) {
        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        if (!formData.UserId) {
            abp.message.warn('Vui lòng chọn tài khoản liên kết!');
            return;
        }

        // Chuyển đổi dữ liệu chuẩn tránh lỗi ép kiểu ở Backend
        formData.userId = parseInt(formData.UserId);
        formData.isPrincipal = $('#isPrincipalCheck').is(':checked');

        if (formData.MainSubjectId) {
            formData.mainSubjectId = parseInt(formData.MainSubjectId);
        } else {
            delete formData.mainSubjectId;
        }

        abp.ui.setBusy(_$form);
        _teacherService.create(formData).done(function () {
            $('#TeacherCreateModal').modal('hide');
            _$form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Thêm hồ sơ giáo viên thành công!');
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        abp.message.confirm('Bạn có chắc muốn xóa giáo viên này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _teacherService.delete({ id: id }).done(function () {
                    dataTable.ajax.reload();
                    abp.notify.success('Đã xóa thành công!');
                });
            }
        });
    });
})(jQuery);