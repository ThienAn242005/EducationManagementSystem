(function ($) {
    var _subjectService = abp.services.app.subject;
    var _$table = $('#SubjectTable');
    var _$form = $('#SubjectCreateForm');

    var editingId = null;
    $('#btnCreateSubject').click(function () {

        editingId = null;

        _$form[0].reset();

        _$form.find('[name="Grade"]').val('');
        _$form.find('[name="Factor"]').val(1);
        _$form.find('[name="NumberOfLessons"]').val(45);

        $('#SubjectModalTitle').text('Thêm Môn học');

        $('#SubjectCreateModal').modal('show');
    });

    $('#btnCloseSubjectModal, #btnCancelSubjectModal').click(function () {
        $('#SubjectCreateModal').modal('hide');
    });
    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],

        listAction: {
            ajaxFunction: _subjectService.getAll
        },

        columnDefs: [
            {
                targets: 0,
                data: 'code'
            },
            {
                targets: 1,
                data: 'name'
            },
            {
                targets: 2,
                data: 'grade',
                render: function (data) {
                    if (data === 1) return 'Khối 10';
                    if (data === 2) return 'Khối 11';
                    if (data === 3) return 'Khối 12';

                    return '';
                }
            },
            {
                targets: 3,
                data: 'factor'
            },
            {
                targets: 4,
                data: 'numberOfLessons'
            },
            {
                targets: 5,
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
    $(document).on('click', '.btn-edit', function () {

        var id = $(this).data('id');

        editingId = id;

        abp.ui.setBusy('#SubjectCreateModal');

        _subjectService.get({
            id: id
        }).done(function (result) {

            _$form.find('[name="Code"]').val(result.code);
            _$form.find('[name="Name"]').val(result.name);
            _$form.find('[name="Grade"]').val(result.grade);
            _$form.find('[name="Factor"]').val(result.factor);
            _$form.find('[name="NumberOfLessons"]').val(result.numberOfLessons);

            $('#SubjectModalTitle').text('Chỉnh sửa Môn học');

            $('#SubjectCreateModal').modal('show');

        }).always(function () {
            abp.ui.clearBusy('#SubjectCreateModal');
        });
    });
    _$form.submit(function (e) {

        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        formData.grade = Number(
            _$form.find('[name="Grade"]').val()
        );

        formData.factor = Number(
            _$form.find('[name="Factor"]').val()
        );

        formData.numberOfLessons = Number(
            _$form.find('[name="NumberOfLessons"]').val()
        );
        if (!editingId) {

            _subjectService.create(formData)
                .done(function () {

                    $('#SubjectCreateModal').modal('hide');

                    _$form[0].reset();

                    dataTable.ajax.reload();

                    abp.notify.success(
                        'Đã thêm môn học!'
                    );
                });

            return;
        }
        formData.id = editingId;

        _subjectService.update(formData)
            .done(function () {

                $('#SubjectCreateModal').modal('hide');

                _$form[0].reset();

                editingId = null;

                dataTable.ajax.reload();

                abp.notify.success(
                    'Đã cập nhật môn học!'
                );
            });
    });
    $(document).on('click', '.btn-delete', function () {

        var id = $(this).data('id');

        abp.message.confirm(
            'Bạn có chắc muốn xóa môn học này?',
            'Xác nhận',
            function (isConfirmed) {

                if (isConfirmed) {

                    _subjectService.delete({
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