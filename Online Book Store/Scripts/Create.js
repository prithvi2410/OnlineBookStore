function validateName(e) {
    const name = document.getElementById('Name').value;
    if (name.length <= 0) {
        e.preventDefault();
        document.getElementById('name-error').innerText = "name cannot be empty";
        document.getElementById('name-error').style.margin = 0;
    }
    else {
        document.getElementById('name-error').innerText = "";
    }
}
function validateEmail(e) {
    const email = document.getElementById('Email').value;
    if (email.length == 0) {
        e.preventDefault();
        document.getElementById('email-error').innerText = "email cannot be empty";
        document.getElementById('email-error').style.margin = 0;
    }
    else if (email.includes("@") == false || email.includes(".") == false) {
        e.preventDefault();
        document.getElementById('email-error').innerText = "invalid email format";
        document.getElementById('email-error').style.margin = 0;
    }
    else {
        document.getElementById('email-error').innerText = "  ";
    }
}
function validateMobileNumber(e) {
    const mobile = document.getElementById('MobileNumber').value;
    if (mobile.length == 0) {
        e.preventDefault();
        document.getElementById('mobile-number-error').innerText = "mobile number cannot be empty";
        document.getElementById('mobile-number-error').style.margin = 0;
    }
    else if (mobile.length != 10 || mobile.charAt(0) < 6) {
        e.preventDefault();
        document.getElementById('mobile-number-error').innerText = "invalid mobile number";
        document.getElementById('mobile-number-error').style.margin = 0;
    }
    else {
        document.getElementById('mobile-number-error').innerText = "  ";
    }
}
function validateUsername(e) {
    const username = document.getElementById('Username').value;
    if (username.length == 0) {
        e.preventDefault();
        document.getElementById('username-error').innerText = "username cannot be empty";
        document.getElementById('username-error').style.margin = 0;
    }
    else if (username.length < 8) {
        e.preventDefault();
        document.getElementById('username-error').innerText = "username should be minimum of 8 characters";
        document.getElementById('username-error').style.margin = 0;
    }
    else {
        document.getElementById('username-error').innerText = "  ";
    }
}
function validatePassword(e) {
    const password = document.getElementById('Password').value;
    if (password.length == 0) {
        e.preventDefault();
        document.getElementById('password-error').innerText = "password cannot be empty";
        document.getElementById('password-error').style.margin = 0;
    }
    else if (password.length < 8) {
        e.preventDefault();
        document.getElementById('password-error').innerText = "password should be minimum of 8 characters";
        document.getElementById('password-error').style.margin = 0;
    }
    else {
        document.getElementById('password-error').innerText = "  ";
    }
}


document.getElementById('submit').addEventListener('click', validateName);
document.getElementById('submit').addEventListener('click', validateEmail);
document.getElementById('submit').addEventListener('click', validateMobileNumber);
document.getElementById('submit').addEventListener('click', validateUsername);
document.getElementById('submit').addEventListener('click', validatePassword);


if (document.getElementsByClassName("field-validation-error").length != 0) {    
    document.getElementById('email-error').style.margin = 0;
    document.getElementById('username-error').style.margin = 0;
}

























