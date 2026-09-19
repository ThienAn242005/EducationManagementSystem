(function ($) {

    var _studentService = abp.services.app.student;
    var _classService = abp.services.app.class;

    var _$table = $('#StudentTable');
    var _$form = $('#StudentCreateForm');

    var editingId = null;

    $('#btnCreateStudent').click(function () {

        editingId = null;
        _$form[0].reset();

        $('#StudentModalTitle').text('Thêm Học sinh');
        $('#StudentCreateModal').modal('show');
    });

    $('#btnCloseStudentModal, #btnCancelStudentModal').click(function () {
        $('#StudentCreateModal').modal('hide');
    });

    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],

        listAction: {
            ajaxFunction: _studentService.getAll
        },

        columnDefs: [
            {
                targets: 0,
                data: 'studentCode'
            },
            {
                targets: 1,
                data: 'fullName'
            },
            {
                targets: 2,
                data: 'gender',
                render: function (data) {
                    return data ? 'Nam' : 'Nữ';
                }
            },
            {
                targets: 3,
                data: 'className',
                defaultContent: '-'
            },
            {
                targets: 4,
                data: 'address',
                defaultContent: '-'
            },
            {
                targets: 5,
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

    $('#StudentCreateModal').on('show.bs.modal', function () {

        _classService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            var $select = $('#StudentClassSelect').empty();

            $select.append(
                new Option('-- Chọn lớp --', '')
            );

            res.items.forEach(function (item) {
                $select.append(
                    new Option(item.className, item.id)
                );
            });
        });
    });

    $(document).on('click', '.btn-edit', function () {

        var id = $(this).data('id');

        editingId = id;

        _studentService.get({
            id: id
        }).done(function (result) {

            $('input[name="StudentCode"]').val(result.studentCode);
            $('input[name="FullName"]').val(result.fullName);
            $('#StudentClassSelect').val(result.classId);
            $('select[name="Gender"]').val(
                result.gender ? 'true' : 'false'
            );
            $('input[name="Address"]').val(result.address);

            $('#StudentModalTitle').text('Chỉnh sửa Học sinh');
            $('#StudentCreateModal').modal('show');
        });
    });

    _$form.submit(function (e) {

        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        formData.studentCode = $('input[name="StudentCode"]').val();
        formData.fullName = $('input[name="FullName"]').val();
        formData.classId = Number($('#StudentClassSelect').val());
        formData.gender = $('select[name="Gender"]').val() === 'true';
        formData.address = $('input[name="Address"]').val();

        if (!formData.classId) {
            abp.message.warn('Vui lòng chọn lớp!');
            return;
        }

        if (!editingId) {

            _studentService.create(formData).done(function () {

                $('#StudentCreateModal').modal('hide');
                _$form[0].reset();

                dataTable.ajax.reload();

                abp.notify.success(
                    'Thêm học sinh thành công!'
                );
            });

            return;
        }

        formData.id = editingId;

        _studentService.update(formData).done(function () {

            $('#StudentCreateModal').modal('hide');
            _$form[0].reset();

            editingId = null;

            dataTable.ajax.reload();

            abp.notify.success(
                'Cập nhật học sinh thành công!'
            );
        });
    });

    $(document).on('click', '.btn-delete', function () {

        var id = $(this).data('id');

        abp.message.confirm(
            'Bạn có chắc muốn xóa học sinh này?',
            'Xác nhận',
            function (isConfirmed) {

                if (isConfirmed) {

                    _studentService.delete({
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