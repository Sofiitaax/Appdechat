# Appdechat
Esta aplicación es un simple chat de consola implementado en C#. Permite enviar y recibir mensajes entre diferentes terminales que se ejecutan en distintos puertos.
#Requisitos
- .NET SDK instalado.
- Un entorno de desarrollo compatible.
#Instalación
1. Clonar este repositorio o descargar.
2. Abrir el proyecto en el entorno de desarrollo de preferencia.
# Uso
1. Se deben abrir múltiples terminales (dos como mínimo), en el entorno de desarrollo elegido.
2. Ejecutar el comando dotnet run <puerto> en cada terminal, reemplazando `<puerto>` con un número de puerto diferente en cada terminal.
    Ejemplo:
    dotnet run 4567
3. Después de iniciar el servidor en cada terminal, el programa pedirá el puerto de destino al cual  se desea enviar un mensaje.
4. Se debe ingresar el número de puerto de la otra terminal y escribir el mensaje que se desea enviar, seguidamente presionar la tecla “enter” para hacer el envio.

    Ejemplo:
    - Terminal 1:
        Ingrese puerto de destino: 44
        Escriba mensaje: Hola desde el puerto 55
    - Terminal 2 (muestra el mensaje recibido):
        Nuevo mensaje recibido: Hola desde el puerto 55
   
Esta es una tarea académica para ilustrar el uso de sockets en C# y la implementación del paradigma de programación orientada a objetos en su forma más básica. 
