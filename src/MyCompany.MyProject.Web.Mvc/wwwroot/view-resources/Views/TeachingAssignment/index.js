(function ($) {
    var _assignService = abp.services.app.teachingAssignment;
    var _teacherService = abp.services.app.teacher;
    var _classService = abp.services.app.class;
    var _subjectService = abp.services.app.subject;
    var _semesterService = abp.services.app.semester;

    var _$table = $('#AssignTable');
    var _$form = $('#AssignForm');
    $('#btnCreateAssign').click(function () {
        $('#AssignModal').modal('show');
    });

    $('#btnCloseAssignModal, #btnCancelAssignModal').click(function () {
        $('#AssignModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],
        listAction: { ajaxFunction: _assignService.getAll },
        columnDefs: [
            { targets: 0, data: 'teacherFullName', defaultContent: '-' },
            { targets: 1, data: 'className', defaultContent: '-' },
            { targets: 2, data: 'subjectName', defaultContent: '-' },
            { targets: 3, data: 'semesterName', defaultContent: '-' },
            {
                targets: 4,
                data: null,
                orderable: false,
                render: function (data, type, row) {
                    return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.id}"><i class="fa fa-trash"></i> Xóa</button>`;
                }
            }
        ]
    });

    $('#AssignModal').on('show.bs.modal', function () {
        _teacherService.getAll({ maxResultCount: 1000 }).done(function (res) {
            $('#AssignTeacherSelect').empty();
            res.items.forEach(t => $('#AssignTeacherSelect').append(new Option(t.fullName, t.id)));
        });
        _classService.getAll({ maxResultCount: 1000 }).done(function (res) {
            $('#AssignClassSelect').empty();
            res.items.forEach(c => $('#AssignClassSelect').append(new Option(c.className, c.id)));
        });
        _subjectService.getAll({ maxResultCount: 1000 }).done(function (res) {
            $('#AssignSubjectSelect').empty();
            res.items.forEach(s => $('#AssignSubjectSelect').append(new Option(s.subjectName, s.id)));
        });
        _semesterService.getAll({ maxResultCount: 1000 }).done(function (res) {
            $('#AssignSemesterSelect').empty();
            res.items.forEach(sem => $('#AssignSemesterSelect').append(new Option(sem.semesterName, sem.id)));
        });
    });

    _$form.submit(function (e) {
        e.preventDefault();
        var formData = _$form.serializeFormToObject();

        _assignService.create(formData).done(function () {
            $('#AssignModal').modal('hide');
            _$form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Phân công giảng dạy thành công!');
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        abp.message.confirm('Bạn có chắc muốn xóa phân công này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _assignService.delete({ id: id }).done(function () {
                    dataTable.ajax.reload();
                    abp.notify.success('Đã xóa thành công!');
                });
            }
        });
    });
})(jQuery);