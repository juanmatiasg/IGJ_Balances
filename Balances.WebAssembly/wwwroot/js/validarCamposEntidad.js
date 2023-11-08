function validarCampos() {
    const busqueda = document.getElementById("busqueda").value;
    const email = document.getElementById("email").value;
    const location = document.getElementById("location").value;
    const fechaDeInicio = document.getElementById("fechaDeInicio").value;
    const fechaDeCierre = document.getElementById("fechaDeCierre").value;

    // Validar que el cuit tiene que ser numerico
    const cuitRegex = /^\d+$/;

    // Email
    const emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;

    if (!busqueda || !location || !fechaDeInicio || !fechaDeCierre) {
        $('#errorModal').modal('show');
    } else if (!emailRegex.test(email)) {
        const modalErrorBody = document.querySelector("#errorModal .modal-body");
        modalErrorBody.textContent = "Por favor, ingrese un correo electrónico válido.";
        $('#errorModal').modal('show');
    } else if (new Date(fechaDeInicio) >= new Date(fechaDeCierre)) {
        const modalErrorBody = document.querySelector("#errorModal .modal-body");
        modalErrorBody.textContent = "La fecha de inicio debe ser anterior a la fecha de cierre.";
        $('#errorModal').modal('show');
    } else {
        if (!cuitRegex.test(busqueda)) {
            const modalErrorBody = document.querySelector("#errorModal .modal-body");
            modalErrorBody.textContent = "El CUIT debe ser un valor numérico.";
            $('#errorModal').modal('show');
        } else {
            // Agrega la lógica para enviar los datos
            window.location.href = '/EstadoContable';
        }
    }
}