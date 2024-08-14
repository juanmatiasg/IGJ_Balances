Inspección General de Justicia - Departamento de Sistemas - Presentación Digital de Balances - Documento Funcional

1. Introducción
Objetivo del Documento
El objetivo de este documento es describir el propósito y los detalles funcionales de la aplicación destinada a generar manifiestos para la presentación de balances de sociedades ante la Inspección General de Justicia (IGJ). Este sistema permite a los usuarios buscar una sociedad por su número correlativo y generar un manifiesto (documento)  que incluye detalles sobre el período correspondiente, datos contables, contador que legaliza, y autoridades responsables, para su ingreso como trámite de Presentación digital de Balances y Asamblea en la IGJ.
Visión General del Sistema
El sistema automatiza el proceso de generación de manifiestos para la presentación de balances ante la IGJ, mejorando la eficiencia y precisión en la preparación de estos documentos clave. Los usuarios pueden generar manifiestos de manera rápida y precisa, asegurando que toda la información relevante esté correctamente documentada.
Audiencia Destinada
Este documento está dirigido al equipo técnico de Calidad y Aseguramiento (QA) del Ministerio de Justicia, quienes serán responsables de validar y verificar el correcto funcionamiento del sistema.

2. Descripción General del Sistema
Arquitectura del Sistema
El sistema sigue una arquitectura cliente-servidor, donde tanto el cliente como el servidor están desarrollados en .NET. El cliente utiliza el framework Blazor para crear interfaces de usuario interactivas y dinámicas. El servidor gestiona las solicitudes del cliente, proporciona los datos necesarios a través de una API REST, y se conecta a una base de datos MongoDB para almacenar y recuperar información.
Diagrama de Arquitectura: El diagrama ilustraría la interacción entre el cliente Blazor, el servidor .NET, la base de datos MongoDB, y el webservice utilizado para buscar sociedades por número correlativo.
Componentes del Sistema
Cliente (Front-End): Desarrollado en Blazor, proporciona la interfaz de usuario donde los usuarios pueden buscar sociedades, generar manifiestos, y visualizar la información necesaria.
Servidor (Back-End): Implementado en .NET, maneja la lógica de negocio, procesa las solicitudes del cliente, interactúa con MongoDB, y genera los manifiestos requeridos.
Base de Datos (MongoDB): Almacena la información relacionada con las sociedades, balances, períodos, autoridades, entre otros datos esenciales para la generación de manifiestos.
Webservice: Consumido por el sistema para buscar y recuperar información de las sociedades por número correlativo.
API REST: Provee los endpoints necesarios para la comunicación entre el cliente y el servidor, asegurando que los datos fluyan de manera segura y eficiente.
Tecnologías Utilizadas
Lenguajes de Programación: C# (para el desarrollo tanto del cliente como del servidor).
Frameworks: .NET y Blazor.
Base de Datos: MongoDB.
Protocolos de Comunicación: HTTP para las solicitudes API y la interacción con el webservice.

3. Requisitos Funcionales
Casos de Uso
Búsqueda de Sociedad: El usuario busca una sociedad por número correlativo.
Generación de Manifiesto: El usuario genera un manifiesto que incluye todos los datos necesarios para poder presentar una vez legalizado ante la IGJ.
Requisitos Detallados
Carátula: Ingreso de domicilio, fechas del período y correo electrónico.
Estados Contables: Carga de datos contables.
Libros: Carga de datos de los libros.
Contador: Datos del contador que legalizó el estado contable.
Autoridades: Datos de las autoridades de la sociedad.
Integrantes: Datos de integrantes, acciones/cuotas, votos y valor nominal.
Documentación: Adjuntar archivos PDF obligatorios.
Confirmación: Generación del manifiesto.

4. Requisitos No Funcionales
Rendimiento
El sistema debe tener un rendimiento óptimo, con tiempos de respuesta rápidos y capacidad de carga para manejar múltiples usuarios concurrentes sin degradar el desempeño.
Seguridad
Actualmente, el sistema utiliza reCaptcha de Google para protegerse contra accesos no autorizados y ataques automatizados. En futuras versiones, se planea integrar Turnstile de Cloudflare.
Además, para asegurar la validez del manifiesto, el sistema genera un ID único para cada balance desde el primer momento. Al generar el manifiesto, se crea un hash que se incluye dentro del documento. Este hash es utilizado posteriormente para verificar la autenticidad del manifiesto durante su presentación, asegurando que el documento no haya sido alterado.
Usabilidad
La interfaz está diseñada para ser intuitiva y fácil de usar, cumpliendo con estándares de accesibilidad web.
Escalabilidad
El sistema está diseñado para ser escalable, permitiendo crecimiento en la base de usuarios y en la cantidad de datos sin comprometer el rendimiento.
Responsividad
El sistema será responsive, adaptándose a diferentes tamaños de pantalla y dispositivos.

5. Descripción de Funcionalidades
Pantallas y Formularios
Carátula: Búsqueda por correlativo y carga de información.
Estados Contables: Formulario para ingresar datos contables.
Libros: Formulario para ingresar datos de libros.
Contador: Formulario para ingresar datos del contador.
Autoridades: Formulario para ingresar datos de autoridades.
Integrantes: Formulario para ingresar datos de integrantes.
Documentación: Sección para adjuntar archivos PDF.
Confirmación: Botón para generar el manifiesto.
Flujos de Trabajo
El proceso se sigue desde la aceptación del acuerdo, búsqueda y carga de datos, hasta la generación del manifiesto.
Reglas de Negocio
La carga de datos debe ser válida y completa antes de permitir la generación del manifiesto.
Los archivos adjuntos deben estar en formato PDF.

6. Interfaz de Usuario
Diseño de la UI
La interfaz gráfica está diseñada para ser fácil de entender. Comienza con la aceptación de un acuerdo, seguida de la carátula donde se busca una sociedad por número correlativo. En esta pantalla se ingresan el domicilio, fechas del período, y el correo electrónico.
Navegación
La barra de navegación contiene las siguientes pestañas:
Carátula
Estados Contables
Libros
Contador
Autoridades
Integrantes
Documentación
Confirmación
 Funcionalidad de SessionStorage
El sistema utiliza sessionStorage para permitir que el usuario abra una nueva pestaña del navegador y comience un nuevo manifiesto sin necesidad de cerrar e iniciar nuevamente el navegador. Esto asegura que la carga de un nuevo manifiesto sea rápida y sencilla, facilitando la gestión de múltiples trámites simultáneamente.
Enlace de Continuación
Al iniciar el trámite, se envía un enlace al correo electrónico proporcionado. Este enlace permite al usuario continuar el proceso en caso de interrupciones, errores de conexión o si el proceso se prolonga más de lo esperado. De esta manera, se asegura que el usuario pueda retomar su trabajo desde el punto en que lo dejó sin pérdida de datos.

7. Interfaz con Otros Sistemas
Integraciones
Webservice: Para buscar sociedades por número correlativo.
Protocolo de Comunicación
API REST: Comunicación entre cliente y servidor.

8. Datos y Modelos de Información
Modelo de Datos
Colección de Balances: Agrupa los datos de los balances y contiene los siguientes componentes:
Session: Información sobre la sesión activa.
Estados Contables: Datos relacionados con los estados contables.
Libros: Información sobre los libros contables.
Contador: Datos del contador que legaliza el estado contable.
Autoridades: Información sobre las autoridades de la sociedad.
Integrantes: Detalles sobre los integrantes de la sociedad, tanto físicos como jurídicos, incluyendo la cantidad de acciones/cuotas, votos y valor nominal.
Archivos: Documentos obligatorios en formato PDF adjuntados al balance.
Diccionario de Datos
Balance: Estructura principal que encapsula toda la información mencionada anteriormente, organizando los datos en los componentes detallados.

9. Pruebas y Validación
Criterios de Aceptación
Todos los datos deben ser ingresados correctamente y el manifiesto debe generarse sin errores.
Plan de Pruebas
Pruebas Unitarias: Validar funciones individuales.
Pruebas de Integración: Verificar interacción entre componentes.
Pruebas de Sistema: Asegurar que el sistema completo funcione como se espera.
Pruebas de Aceptación: Confirmar que el sistema cumple con los requisitos del usuario.
Casos de Prueba
Caso 1: Buscar sociedad por número correlativo.
Caso 2: Generar manifiesto con datos válidos.
Caso 3: Manejar errores durante la carga de datos.

10. Mantenimiento y Soporte
Procedimientos de Mantenimiento
Actualizaciones: Procedimientos para actualizar el sistema y aplicar parches.
Monitoreo: Herramientas para monitorear el rendimiento y la estabilidad.
Plan de Soporte
Incidentes: Protocolo para manejar y resolver problemas técnicos.
Documentación: Guías para la resolución de problemas comunes.

11. Anexos
Glosario
API REST: Interfaz de Programación de Aplicaciones basada en el protocolo HTTP.
Blazor: Framework de .NET para crear aplicaciones web interactivas.

Referencias
Documentos de Normativas: Referencias a normativas y estándares relacionados con la presentación de balances.
Historial de Cambios
Versión 1: Documento inicial con detalles de la aplicación.
