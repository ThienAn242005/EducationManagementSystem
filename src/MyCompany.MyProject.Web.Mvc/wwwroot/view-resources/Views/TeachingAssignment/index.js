(function ($) {

    var _assignService = abp.services.app.teachingAssignment;
    var _teacherService = abp.services.app.teacher;
    var _classService = abp.services.app.class;
    var _subjectService = abp.services.app.subject;
    var _semesterService = abp.services.app.semester;

    var _$table = $('#AssignTable');
    var _$form = $('#AssignForm');

    var editingId = null;
    var allClasses = [];

    $('#btnCreateAssign').click(function () {
        editingId = null;
        _$form[0].reset();

        $('#AssignModalTitle').text('Thêm Phân công Giáo viên');
        $('#AssignModal').modal('show');
    });

    $('#btnCloseAssignModal, #btnCancelAssignModal').click(function () {
        $('#AssignModal').modal('hide');
    });

    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],

        listAction: {
            ajaxFunction: _assignService.getAll,
            inputFilter: function () {
                return {
                    filter: $('#AssignSearch').val()
                };
            }
        },

        columnDefs: [
            {
                targets: 0,
                data: 'teacherFullName',
                defaultContent: '-'
            },
            {
                targets: 1,
                data: 'className',
                defaultContent: '-'
            },
            {
                targets: 2,
                data: 'subjectName',
                defaultContent: '-'
            },
            {
                targets: 3,
                data: 'semesterName',
                defaultContent: '-'
            },
            {
                targets: 4,
                data: null,
                orderable: false,
                render: function (data, type, row) {
                    return `
                        <button class="btn btn-sm btn-warning btn-edit" data-id="${row.id}">
                            <i class="fa fa-edit"></i> Sửa
                        </button>
                        <button class="btn btn-sm btn-danger btn-delete" data-id="${row.id}">
                            <i class="fa fa-trash"></i> Xóa
                        </button>
                    `;
                }
            }
        ]
    });

    $('#AssignModal').on('show.bs.modal', function () {

        _teacherService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            $('#AssignTeacherSelect').empty();
            $('#AssignTeacherSelect').append(
                new Option('-- Chọn giáo viên --', '')
            );

            res.items.forEach(function (teacher) {
                $('#AssignTeacherSelect').append(
                    new Option(teacher.fullName, teacher.id)
                );
            });
        });

        _classService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            allClasses = res.items;

            $('#AssignClassSelect').empty();
            $('#AssignClassSelect').append(
                new Option('-- Chọn lớp --', '')
            );

            allClasses.forEach(function (item) {
                $('#AssignClassSelect').append(
                    new Option(item.className, item.id)
                );
            });
        });

        _subjectService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            $('#AssignSubjectSelect').empty();
            $('#AssignSubjectSelect').append(
                new Option('-- Chọn môn --', '')
            );

            res.items.forEach(function (subject) {

                var option = new Option(
                    subject.name,
                    subject.id
                );

                $(option).data('grade', subject.grade);

                $('#AssignSubjectSelect').append(option);
            });
        });

        _semesterService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            $('#AssignSemesterSelect').empty();
            $('#AssignSemesterSelect').append(
                new Option('-- Chọn học kỳ --', '')
            );

            res.items.forEach(function (semester) {
                $('#AssignSemesterSelect').append(
                    new Option(semester.name, semester.id)
                );
            });
        });
    });

    $('#AssignSubjectSelect').change(function () {

        var grade = $(this)
            .find('option:selected')
            .data('grade');

        $('#AssignClassSelect').empty();

        $('#AssignClassSelect').append(
            new Option('-- Chọn lớp --', '')
        );

        if (grade === undefined) {
            return;
        }

        allClasses
            .filter(function (item) {
                return item.grade === grade;
            })
            .forEach(function (item) {
                $('#AssignClassSelect').append(
                    new Option(item.className, item.id)
                );
            });
    });

    $(document).on('click', '.btn-edit', function () {

        var id = $(this).data('id');

        editingId = id;

        abp.ui.setBusy('#AssignModal');

        _assignService.get({
            id: id
        }).done(function (result) {

            $('#AssignTeacherSelect').val(result.teacherId);
            $('#AssignSubjectSelect').val(result.subjectId);

            $('#AssignSubjectSelect').trigger('change');

            $('#AssignClassSelect').val(result.classId);
            $('#AssignSemesterSelect').val(result.semesterId);

            $('#AssignModalTitle').text(
                'Chỉnh sửa Phân công Giáo viên'
            );

            $('#AssignModal').modal('show');

        }).always(function () {
            abp.ui.clearBusy('#AssignModal');
        });
    });

    _$form.submit(function (e) {

        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        formData.teacherId = Number(
            $('#AssignTeacherSelect').val()
        );

        formData.classId = Number(
            $('#AssignClassSelect').val()
        );

        formData.subjectId = Number(
            $('#AssignSubjectSelect').val()
        );

        formData.semesterId = Number(
            $('#AssignSemesterSelect').val()
        );

        if (!formData.teacherId ||
            !formData.classId ||
            !formData.subjectId ||
            !formData.semesterId) {

            abp.notify.warn('Vui lòng chọn đầy đủ thông tin!');
            return;
        }

        if (!editingId) {

            _assignService.create(formData).done(function () {

                $('#AssignModal').modal('hide');
                _$form[0].reset();

                dataTable.ajax.reload();

                abp.notify.success(
                    'Phân công giảng dạy thành công!'
                );
            });

            return;
        }

        formData.id = editingId;

        _assignService.update(formData).done(function () {

            $('#AssignModal').modal('hide');
            _$form[0].reset();

            editingId = null;

            dataTable.ajax.reload();

            abp.notify.success(
                'Cập nhật phân công thành công!'
            );
        });
    });

    $('#btnSearchAssign').click(function () {
        dataTable.ajax.reload();
    });

    $('#AssignSearch').keypress(function (e) {

        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $(document).on('click', '.btn-delete', function () {

        var id = $(this).data('id');

        abp.message.confirm(
            'Bạn có chắc muốn xóa phân công này?',
            'Xác nhận',
            function (isConfirmed) {

                if (isConfirmed) {

                    _assignService.delete({
                        id: id
                    }).done(function () {

                        dataTable.ajax.reload();

                        abp.notify.success(
                            'Đã xóa thành công!'
                        );
                    });
                }
            }
        );
    });

})(jQuery);