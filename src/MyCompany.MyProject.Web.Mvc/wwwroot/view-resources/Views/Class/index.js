(function ($) {
    var _classService = abp.services.app.class;

    var _$table = $('#ClassTable');
    var _$form = $('#ClassCreateForm');

    var editingId = null;

    // Create
    $('#btnCreateClass').click(function () {
        editingId = null;

        _$form[0].reset();

        $('#ClassModalTitle').text('Thêm Lớp học');

        $('#ClassCreateModal').modal('show');
    });

    // Close
    $('#btnCloseClassModal, #btnCancelClassModal').click(function () {
        $('#ClassCreateModal').modal('hide');
    });

    // DataTable
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],

        listAction: {
            ajaxFunction: _classService.getAll,
            inputFilter: function () {
                return {};
            }
        },

        columnDefs: [
            {
                targets: 0,
                data: 'className'
            },
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
                    return `
                        <button class="btn btn-sm btn-warning btn-edit"
                                data-id="${row.id}">
                            <i class="fa fa-edit"></i> Sửa
                        </button>

                        <button class="btn btn-sm btn-danger btn-delete"
                                data-id="${row.id}">
                            <i class="fa fa-trash"></i> Xóa
                        </button>
                    `;
                }
            }
        ]
    });

    // Edit
    $(document).on('click', '.btn-edit', function () {
        var id = $(this).data('id');

        editingId = id;

        abp.ui.setBusy('#ClassCreateModal');

        _classService.get({
            id: id
        }).done(function (result) {

            _$form.find('[name="ClassName"]').val(result.className);
            _$form.find('[name="Grade"]').val(result.grade);

            $('#ClassModalTitle').text('Chỉnh sửa Lớp học');

            $('#ClassCreateModal').modal('show');

        }).always(function () {
            abp.ui.clearBusy('#ClassCreateModal');
        });
    });

    // Create / Update
    _$form.submit(function (e) {
        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        formData.grade = parseInt(
            _$form.find('[name="Grade"]').val()
        );

        if (!editingId) {

            _classService.create(formData).done(function () {
                $('#ClassCreateModal').modal('hide');

                _$form[0].reset();

                dataTable.ajax.reload();

                abp.notify.success('Thêm lớp học thành công!');
            });

            return;
        }

        formData.id = editingId;

        _classService.update(formData).done(function () {
            $('#ClassCreateModal').modal('hide');

            _$form[0].reset();

            editingId = null;

            dataTable.ajax.reload();

            abp.notify.success('Cập nhật lớp học thành công!');
        });
    });

    // Delete
    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');

        abp.message.confirm(
            'Bạn có chắc muốn xóa lớp này?',
            'Xác nhận',
            function (isConfirmed) {
                if (isConfirmed) {
                    _classService.delete({
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