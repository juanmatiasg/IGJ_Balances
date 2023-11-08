document.addEventListener('DOMContentLoaded', function () {
    // Recuperar los valores de sessionStorage
    var razonSocial = sessionStorage.getItem('RazonSocial');
    var tipoEntidad = sessionStorage.getItem('TipoEntidad');

    // Verificar si los valores existen en sessionStorage
    if (razonSocial && tipoEntidad) {
        // Actualizar los elementos HTML con los valores recuperados
        document.getElementById('razonSocial').textContent = razonSocial;
        document.getElementById('tipoEntidad').textContent = tipoEntidad;
    }
});