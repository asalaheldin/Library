$(document).ready(function () {
    $('#sendEmails').on("click", function () {
        $.ajax({
            url: '/book/reminder',
            async: false,
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if(data.result === 'success')
                {
                    alert('Reminders are on their way!');
                }
                else
                {
                    alert('Something went wrong, kindly check with the system admin!');
                }
            }
        });
    });
    $('.table').dataTable({
        rowReorder: true,
        bDestroy: true,
        language: {
            processing: "Ongoing treatment",
            search: "Search",
            lengthMenu: "Show _MENU_ entries",
            info: "Show _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "Show 0 to 0 of 0 entries",
            zeroRecords: "No data available in table",
            emptyTable: "No data available in table",
            paginate: {
                first: "First",
                previous: "Previous",
                next: "Next",
                last: "Last"
            },
            aria: {
                sortAscending: "Enable to sort the column in ascending order",
                sortDescending: "Enable to sort the column in descending order"
            }
        }
    })
});

$('.DropDownfilterItems').change(function () {
    var $this = $(this);
    var dropurl = $this.attr('url');
    $table = $('.table');
    $.ajax({
        url: dropurl,
        async: false,
        data: { Id: $this.val() },
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            $table.dataTable().fnClearTable();
            $table.dataTable().fnDraw();
            $table.dataTable().fnDestroy();
            $table.find('tbody').empty().html(data.partialView);
            $table.dataTable({
                rowReorder: true,
                bDestroy: true,
                language: {
                    processing: "Ongoing treatment",
                    search: "Search",
                    lengthMenu: "Show _MENU_ entries",
                    info: "Show _START_ to _END_ of _TOTAL_ entries",
                    infoEmpty: "Show 0 to 0 of 0 entries",
                    zeroRecords: "No data available in table",
                    emptyTable: "No data available in table",
                    paginate: {
                        first: "First",
                        previous: "Previous",
                        next: "Next",
                        last: "Last"
                    },
                    aria: {
                        sortAscending: "Enable to sort the column in ascending order",
                        sortDescending: "Enable to sort the column in descending order"
                    }
                }
            })
        }
    });
});
$('.DropDownfilterItems').trigger('change');