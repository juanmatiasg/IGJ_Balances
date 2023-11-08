
        // Función para manejar la acción cuando se marca/desmarca el checkbox
 const handleAccept = async () => {
            const balanceId = localStorage.getItem("BalanceId");

    console.log("BalanceId: " + localStorage.getItem("BalanceId"));

    const mensaje = {
        balanceId: balanceId,
    aceptado: document.getElementById('Aceptar').checked, // Obtener el valor del checkbox directamente
            };

     const urlTerminos = 'https://localhost:7274/terminos'; // Reemplaza 'URL_DE_TU_API' con la URL de tu API

    console.log("emitiendo aceptado: " + urlTerminos);

    window.location.href = '/Entidad'; // Reemplaza 'Entidad.cshtml' con la URL de tu página destino




    try {
        await axios.post(urlTerminos, mensaje);
    // Deshabilita el checkbox después de enviar la solicitud
    document.getElementById('Aceptar').disabled = true;
    console.log("emitido por el checkbox");

            } catch (error) {
        console.error('Error al enviar la aceptación:', error);
            }
        };

        // Llama a la función para obtener el Marco Legal cuando se carga la página
        window.addEventListener('load', () => {
            const urlGet = 'https://localhost:7274/parametro/NORMATIVA_BALANCES'; // Reemplaza 'URL_DE_TU_API' con la URL de tu API
    console.log("Buscando parametro: " + urlGet);
    axios
    .get(urlGet)
                .then((response) => {
        document.getElementById('MarcoLegalText').value = response.data.texto;
                })
                .catch((error) => {
        console.log(error.response);
                });
        });
