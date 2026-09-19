(function ($) {
    var _studentService = abp.services.app.student;
    var _classService = abp.services.app.class;
    var _$table = $('#StudentTable');
    var _$form = $('#StudentCreateForm');
    $('#btnCreateStudent').click(function () {
        $('#StudentCreateModal').modal('show');
    });

    $('#btnCloseStudentModal, #btnCancelStudentModal').click(function () {
        $('#StudentCreateModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: { ajaxFunction: _studentService.getAll },
        columnDefs: [
            { targets: 0, data: 'studentCode' },
            { targets: 1, data: 'fullName' },
            {
                targets: 2,
                data: 'gender',
                render: function (data) { return data ? 'Nam' : 'Nữ'; }
            },
            { targets: 3, data: 'className', defaultContent: '-' },
            { targets: 4, data: 'address', defaultContent: '-' },
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

    $('#StudentCreateModal').on('show.bs.modal', function () {
        _classService.getAll({ maxResultCount: 1000 }).done(function (res) {
            $('#StudentClassSelect').empty();
            res.items.forEach(c => $('#StudentClassSelect').append(new Option(c.className, c.id)));
        });
    });

    _$form.submit(function (e) {
        e.preventDefault();
        var formData = _$form.serializeFormToObject();
        formData.gender = formData.Gender === 'true';

        _studentService.create(formData).done(function () {
            $('#StudentCreateModal').modal('hide');
            _$form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Thêm học sinh thành công!');
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        abp.message.confirm('Bạn có chắc muốn xóa học sinh này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _studentService.delete({ id: id }).done(function () {
                    dataTable.ajax.reload();
                    abp.notify.success('Đã xóa thành công!');
                });
            }
        });
    });
})(jQuery);