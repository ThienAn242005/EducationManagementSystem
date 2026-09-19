(function ($) {
    var _subjectService = abp.services.app.subject;
    var _$table = $('#SubjectTable');
    var _$form = $('#SubjectCreateForm');
    $('#btnCreateSubject').click(function () {
        $('#SubjectCreateModal').modal('show');
    });

    $('#btnCloseSubjectModal, #btnCancelSubjectModal').click(function () {
        $('#SubjectCreateModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],
        listAction: { ajaxFunction: _subjectService.getAll },
        columnDefs: [
            { targets: 0, data: 'code' },
            { targets: 1, data: 'name' },
            { targets: 2, data: 'factor' },
            {
                targets: 3,
                data: null,
                orderable: false,
                render: function (data, type, row) {
                    return `<button class="btn btn-sm btn-danger btn-delete" data-id="${row.id}"><i class="fa fa-trash"></i> Xóa</button>`;
                }
            }
        ]
    });

    _$form.submit(function (e) {
        e.preventDefault();
        var formData = _$form.serializeFormToObject();
        _subjectService.create(formData).done(function () {
            $('#SubjectCreateModal').modal('hide');
            _$form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Đã thêm môn học!');
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        abp.message.confirm('Bạn có chắc muốn xóa môn học này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _subjectService.delete({ id: id }).done(function () {
                    dataTable.ajax.reload();
                    abp.notify.success('Đã xóa thành công!');
                });
            }
        });
    });
})(jQuery);