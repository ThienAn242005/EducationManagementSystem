(function ($) {
    var _semesterService = abp.services.app.semester;
    var _$table = $('#SemesterTable');
    var _$form = $('#SemesterCreateForm');

    var editingId = null;

    // Create
    $('#btnCreateSemester').click(function () {
        editingId = null;

        _$form[0].reset();

        _$form.find('[name="SemesterNumber"]').val(1);
        _$form.find('[name="StartYear"]').val(2026);
        $('#isCurrentSem').prop('checked', false);

        $('#SemesterModalTitle').text('Thêm Học kỳ');

        $('#SemesterCreateModal').modal('show');
    });

    // Close
    $('#btnCloseSemesterModal, #btnCancelSemesterModal').click(function () {
        $('#SemesterCreateModal').modal('hide');
    });

    // DataTable
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],

        listAction: {
            ajaxFunction: _semesterService.getAll
        },

        columnDefs: [
            {
                targets: 0,
                data: 'name'
            },
            {
                targets: 1,
                data: 'semesterNumber',
                render: function (data) {
                    return data === 1 ? 'Học kỳ 1' : 'Học kỳ 2';
                }
            },
            {
                targets: 2,
                data: 'startYear'
            },
            {
                targets: 3,
                data: 'isCurrent',
                render: function (data) {
                    return data
                        ? '<span class="badge badge-success">Đang hoạt động</span>'
                        : '<span class="badge badge-secondary">Đã kết thúc</span>';
                }
            },
            {
                targets: 4,
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

        abp.ui.setBusy('#SemesterCreateModal');

        _semesterService.get({
            id: id
        }).done(function (result) {

            _$form.find('[name="Name"]').val(result.name);
            _$form.find('[name="SemesterNumber"]').val(result.semesterNumber);
            _$form.find('[name="StartYear"]').val(result.startYear);
            $('#isCurrentSem').prop('checked', result.isCurrent);

            $('#SemesterModalTitle').text('Chỉnh sửa Học kỳ');

            $('#SemesterCreateModal').modal('show');

        }).always(function () {
            abp.ui.clearBusy('#SemesterCreateModal');
        });
    });

    // Create / Update
    _$form.submit(function (e) {
        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        formData.semesterNumber = Number(
            _$form.find('[name="SemesterNumber"]').val()
        );

        formData.startYear = Number(
            _$form.find('[name="StartYear"]').val()
        );

        formData.isCurrent = $('#isCurrentSem').is(':checked');

        if (isNaN(formData.semesterNumber)) {
            abp.notify.error('Học kỳ không hợp lệ!');
            return;
        }

        if (isNaN(formData.startYear)) {
            abp.notify.error('Năm bắt đầu không hợp lệ!');
            return;
        }

        // Create
        if (!editingId) {

            _semesterService.create(formData).done(function () {
                $('#SemesterCreateModal').modal('hide');

                _$form[0].reset();

                dataTable.ajax.reload();

                abp.notify.success('Đã thêm học kỳ!');
            });

            return;
        }

        // Update
        formData.id = editingId;

        _semesterService.update(formData).done(function () {
            $('#SemesterCreateModal').modal('hide');

            _$form[0].reset();

            editingId = null;

            dataTable.ajax.reload();

            abp.notify.success('Đã cập nhật học kỳ!');
        });
    });

    // Delete
    $(document).on('click', '.btn-delete', function () {
        var id = $(this).data('id');

        abp.message.confirm(
            'Bạn có chắc muốn xóa học kỳ này?',
            'Xác nhận',
            function (isConfirmed) {
                if (isConfirmed) {
                    _semesterService.delete({
                        id: id
                    }).done(function () {
                        dataTable.ajax.reload();

                        abp.notify.success('Đã xóa thành công!');
                    });
                }
            }
        );
    });

})(jQuery);