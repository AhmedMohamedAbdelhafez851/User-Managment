$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var button = $(this);
        var userId = button.data('id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        if (confirm('Are you sure you want to delete this user?')) {
            $.ajax({
                url: '/api/Users?userId=' + userId,
                type: 'DELETE',
                headers: {
                    'RequestVerificationToken': token
                },
                success: function (result) {
                    button.closest('tr').remove();
                },
                error: function (xhr, status, error) {
                    console.log(xhr.responseText); // Log the response for debugging
                    alert('An error occurred while deleting the user.');
                }
            });
        }
    });
});
