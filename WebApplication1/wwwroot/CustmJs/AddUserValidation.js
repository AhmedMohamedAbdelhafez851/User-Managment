//$(document).ready(function () {
//    var isEmailAvailable = false;
//    var isUserNameAvailable = false;

//    $('#UserName').blur(function () {
//        var userName = $(this).val();
//        $.post('@Url.Action("IsUserNameAvailable")', { userName: userName }, function (data) {
//            if (!data) {
//                $('#UserNameError').text('Username already exists.');
//                isUserNameAvailable = false;
//            } else {
//                $('#UserNameError').text('');
//                isUserNameAvailable = true;
//            }
//        });
//    });

//    $('#Email').blur(function () {
//        var email = $(this).val();
//        $.post('@Url.Action("IsEmailAvailable")', { email: email }, function (data) {
//            if (!data) {
//                $('#EmailError').text('Email already exists.');
//                isEmailAvailable = false;
//            } else {
//                $('#EmailError').text('');
//                isEmailAvailable = true;
//            }
//        });
//    });

//    function validatePassword() {
//        var password = $('#Password').val();
//        var capitalLetterPattern = /[A-Z]/;
//        if (!capitalLetterPattern.test(password)) {
//            $('#PasswordError').text('Password must contain at least one capital letter.');
//            return false;
//        } else {
//            $('#PasswordError').text('');
//            return true;
//        }
//    }

//    $('#Password').blur(validatePassword);
//    $('#Password').on('input', validatePassword);

//    $('#userForm').submit(function (e) {
//        if (!validatePassword() || !isEmailAvailable || !isUserNameAvailable) {
//            e.preventDefault(); // Prevent form submission
//            if (!isUserNameAvailable) {
//                $('#UserNameError').text('Username already exists.');
//            }
//            if (!isEmailAvailable) {
//                $('#EmailError').text('Email already exists.');
//            }
//        }
//    });
//});
