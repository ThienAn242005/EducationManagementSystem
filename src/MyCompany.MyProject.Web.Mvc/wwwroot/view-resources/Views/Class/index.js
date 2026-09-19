(function ($) {
    var _classService = abp.services.app.class;
    var _$table = $('#ClassTable');
    var _$form = $('#ClassCreateForm');
    $('#btnCreateClass').click(function () {
        $('#ClassCreateModal').modal('show');
    });

    $('#btnCloseClassModal, #btnCancelClassModal').click(function () {
        $('#ClassCreateModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _classService.getAll,
            inputFilter: function () { return {}; }
        },
        columnDefs: [
            { targets: 0, data: 'className' },
            {
                targets: 1,
                data: 'grade',
                render: function (data) {
                    if (data === 1) return 'Khối 10';
                    if (data === 2) return 'Khối 11';
                    if (data === 3) return 'Khối 12';
                    return '-';
                }
            },
            {
                targets: 2,
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

        formData.GradeLevel = parseInt(formData.GradeLevel);

        console.log('Payload:', formData);
        console.log('grade:', formData.GradeLevel);

        _classService.create(formData).done(function () {
            $('#ClassCreateModal').modal('hide');
            _$form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Thêm lớp học thành công!');
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        abp.message.confirm('Bạn có chắc muốn xóa lớp này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _classService.delete({ id: id }).done(function () {
                    dataTable.ajax.reload();
                    abp.notify.success('Đã xóa thành công!');
                });
            }
        });
    });
})(jQuery);