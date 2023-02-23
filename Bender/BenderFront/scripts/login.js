async function login(userName, password) {
    $("#login").prop('disabled', true);
    try {
        response = await ajaxlogin(userName, password);
    } catch (e) {
        LoginFail();
        return;
    }
    if (response.success) {
        sessionStorage.setItem("rol", response.rolId);
        location.href = "main.html";
    } else {
        LoginFail();
    }
}

function LoginFail() {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener("mouseenter", Swal.stopTimer);
            toast.addEventListener("mouseleave", Swal.resumeTimer);
        },
    });

    Toast.fire({
        icon: "error",
        title: "Credenciales incorrectas",
    });

    $("#username").val("");
    $("#password").val("");
    $("#spinnerLogin").hide();
    $("#login").prop('disabled', false);
}
