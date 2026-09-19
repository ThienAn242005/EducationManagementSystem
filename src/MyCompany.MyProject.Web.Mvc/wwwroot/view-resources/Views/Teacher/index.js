(function ($) {

    var _teacherService = abp.services.app.teacher;
    var _subjectService = abp.services.app.subject;
    var _userService = abp.services.app.user;

    var _$table = $('#TeacherTable');
    var _$form = $('#TeacherCreateForm');
    var editingId = null;

    $('#btnCreateTeacher').click(function () {
        editingId = null;
        _$form[0].reset();

        $('#TeacherModalTitle').text('Thêm Giáo viên');
        $('#TeacherCreateModal').modal('show');
    });

    $('#btnCloseTeacherModal, #btnCancelTeacherModal').click(function () {
        $('#TeacherCreateModal').modal('hide');
    });

    var dataTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        order: [],

        listAction: {
            ajaxFunction: _teacherService.getAll
        },

        columnDefs: [
            {
                targets: 0,
                data: 'teacherCode'
            },
            {
                targets: 1,
                data: 'fullName'
            },
            {
                targets: 2,
                data: 'phoneNumber',
                defaultContent: '-'
            },
            {
                targets: 3,
                data: 'mainSubjectName',
                defaultContent: '-'
            },
            {
                targets: 4,
                data: 'isPrincipal',
                render: function (data) {
                    return data
                        ? '<span class="badge badge-danger">Hiệu trưởng</span>'
                        : '<span class="badge badge-info">Giáo viên</span>';
                }
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

    $('#TeacherCreateModal').on('show.bs.modal', function () {

        _userService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            var $select = $('#TeacherUserSelect').empty();

            $select.append(
                new Option('-- Chọn tài khoản --', '')
            );

            res.items.forEach(function (user) {
                $select.append(
                    new Option(
                        user.fullName + ' (' + user.userName + ')',
                        user.id
                    )
                );
            });
        });

        _subjectService.getAll({
            maxResultCount: 1000
        }).done(function (res) {

            var $select = $('#TeacherSubjectSelect').empty();

            $select.append(
                new Option('-- Không chọn --', '')
            );

            res.items.forEach(function (subject) {
                $select.append(
                    new Option(subject.name, subject.id)
                );
            });
        });
    });

    $(document).on('click', '.btn-edit', function () {

        var id = $(this).data('id');

        editingId = id;

        _teacherService.get({
            id: id
        }).done(function (result) {

            $('#TeacherUserSelect').val(result.userId);
            $('input[name="TeacherCode"]').val(result.teacherCode);
            $('input[name="FullName"]').val(result.fullName);
            $('input[name="PhoneNumber"]').val(result.phoneNumber);
            $('#TeacherSubjectSelect').val(result.mainSubjectId);
            $('#isPrincipalCheck').prop('checked', result.isPrincipal);

            $('#TeacherModalTitle').text('Chỉnh sửa Giáo viên');
            $('#TeacherCreateModal').modal('show');
        });
    });

    _$form.submit(function (e) {

        e.preventDefault();

        var formData = _$form.serializeFormToObject();

        formData.userId = Number($('#TeacherUserSelect').val());
        formData.teacherCode = $('input[name="TeacherCode"]').val();
        formData.fullName = $('input[name="FullName"]').val();
        formData.phoneNumber = $('input[name="PhoneNumber"]').val();
        formData.mainSubjectId = Number($('#TeacherSubjectSelect').val());
        formData.isPrincipal = $('#isPrincipalCheck').is(':checked');

        if (!formData.userId) {
            abp.message.warn('Vui lòng chọn tài khoản!');
            return;
        }

        if (!editingId) {

            _teacherService.create(formData).done(function () {

                $('#TeacherCreateModal').modal('hide');
                _$form[0].reset();

                dataTable.ajax.reload();

                abp.notify.success(
                    'Thêm giáo viên thành công!'
                );
            });

            return;
        }

        formData.id = editingId;

        _teacherService.update(formData).done(function () {

            $('#TeacherCreateModal').modal('hide');
            _$form[0].reset();

            editingId = null;

            dataTable.ajax.reload();

            abp.notify.success(
                'Cập nhật giáo viên thành công!'
            );
        });
    });

    $(document).on('click', '.btn-delete', function () {

        var id = $(this).data('id');

        abp.message.confirm(
            'Bạn có chắc muốn xóa giáo viên này?',
            'Xác nhận',
            function (isConfirmed) {

                if (isConfirmed) {

                    _teacherService.delete({
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