(function ($) {
    var _semesterService = abp.services.app.semester;
    var _$table = $('#SemesterTable');
    var _$form = $('#SemesterCreateForm');
    $('#btnCreateSemester').click(function () {
        $('#SemesterCreateModal').modal('show');
    });

    $('#btnCloseSemesterModal, #btnCancelSemesterModal').click(function () {
        $('#SemesterCreateModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],
        listAction: { ajaxFunction: _semesterService.getAll },
        columnDefs: [
            { targets: 0, data: 'name' },
            { targets: 1, data: 'startYear' },
            {
                targets: 2,
                data: 'isCurrent',
                render: function (data) {
                    return data ? '<span class="badge badge-success">Đang hoạt động</span>' : '<span class="badge badge-secondary">Đã kết thúc</span>';
                }
            },
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
        formData.isCurrent = $('#isCurrentSem').is(':checked');

        _semesterService.create(formData).done(function () {
            $('#SemesterCreateModal').modal('hide');
            _$form[0].reset();
            dataTable.ajax.reload();
            abp.notify.success('Đã thêm học kỳ!');
        });
    });

    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');
        abp.message.confirm('Bạn có chắc muốn xóa học kỳ này?', 'Xác nhận', function (isConfirmed) {
            if (isConfirmed) {
                _semesterService.delete({ id: id }).done(function () {
                    dataTable.ajax.reload();
                    abp.notify.success('Đã xóa thành công!');
                });
            }
        });
    });
})(jQuery);