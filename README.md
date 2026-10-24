# Trabajo Tarjeta 2026

## Integrantes del grupo
 - Deheza Ana
 - Segovia Julieta

## Aclaraciones
El siguiente trabajo es un enunciado iterativo. Regularmente se ampliará y/o modificará el enunciado.

[ENTREGA](https://forms.gle/NeM1adptvvtioRJH9) (El form cierra el 21/9)

- El trabajo debe implementarse en .NET, usando Git para el control de versiones y NUnit como framework de testing.
- Los tests unitarios no deben depender de la base de datos real. Tienen [este ejercicio](https://github.com/mgonzalesips/Tienda) de ejemplo para ver como hacerlo
- **Todos** los métodos deben estar testeados con un test unitario, aunque no se aclare explícitamente en el enunciado.
- Para la nota final se tomará en cuenta no solo el código fuente de la implementación, sino también el uso de Git y las herramientas que este provee como commits, ramas y tags ademas de los tests.
- Cada clase de la implementación y de testing debe estar en un archivo aparte.

---

## Iteración 1

Escribir un programa con programación orientada a objetos que permita ilustrar el funcionamiento del transporte urbano de pasajeros de la ciudad de Rosario.
Las clases que interactúan en la simulación son: Colectivo, Tarjeta y Boleto.
Cuando un usuario viaja en colectivo con una tarjeta, se genera un boleto como resultado de la operación `colectivo.pagarCon(tarjeta)`.

Para esta iteración se consideran los siguientes supuestos:

- No hay medio boleto de ningún tipo.
- No hay transbordos.
- No hay saldo negativo.
- La tarifa básica de un pasaje es de: $1580
- Las cargas aceptadas de tarjetas son: (2000, 3000, 4000, 5000, 8000, 10000, 15000, 20000, 25000, 30000)
- El límite de saldo de una tarjeta es de $40000

Se pide:

- Completar los nombres de cada integrante al principio del enunciado. 
- Hacer un fork del repositorio.
- Crear un `DbContext` con los `DbSet` correspondientes a Tarjeta, Colectivo y Boleto.
- Implementar el código de las clases Tarjeta, Colectivo y Boleto.
- Hacer que el test Tarjeta.cs funcione correctamente con todos los montos de pago listados.
- Enviar el enlace del repositorio al mail del profesor con los integrantes del grupo: dos por grupo.

## Iteración 2
 
Para esta iteración hay 3 tareas principales. Crear un issue en GitHub copiando la descripción de cada tarea y completar cada uno en una rama diferente. Éstas serán mergeadas al validar, luego de una revisión cruzada (de ambos integrantes del grupo), que todo el código tiene sentido y está correctamente implementado.
 
No es necesario que todo el código para un issue esté funcionando al 100% antes de mergearlo, pueden crear pull requests que solucionen algún ítem particular del problema para avanzar más rápido.
 
Además de las tareas planteadas, cada grupo tiene tareas pendientes de la iteración anterior que debe finalizar antes de comenzar con la iteración 2.
 
### Descuento de saldos
 
Cada vez que una tarjeta paga un boleto, descuenta el valor del monto gastado.
 
- Si la tarjeta se queda sin saldo, la operación `colectivo.pagarCon(tarjeta)` devuelve `false`.
### Saldo negativo
 
- Si la tarjeta se queda sin crédito, puede tener un saldo negativo de hasta $2000.
- Cuando se vuelve a cargar la tarjeta, se descuenta el saldo de lo que se haya consumido.
- Escribir un test que valide que la tarjeta no pueda quedar con menos saldo que el permitido.
- Escribir un test que valide que el saldo de la tarjeta descuenta correctamente el/los viaje/s plus otorgado/s.
### Franquicia de Boleto
 
Existen dos tipos de franquicia en lo que refiere a tarjetas, las franquicias parciales, como el medio boleto estudiantil o el universitario, y las completas como las de jubilados (Notar que también existe boleto gratuito para estudiantes).
 
- Crear la clase `TarjetaTipo` y relacionarla con `Tarjeta` mediante una clave foránea. Los tipos a representar son: Normal, Medio boleto estudiantil, Boleto gratuito estudiantil y Franquicia completa. Ustedes deciden qué atributos necesita `TarjetaTipo` para que cada tarjeta pueda calcular el precio de su pasaje.
- Agregar un tipo de tarjeta nuevo no debe requerir crear una clase nueva.
- Para esta iteración considerar simplemente que cuando se paga con una tarjeta del tipo MedioBoleto el costo del pasaje vale la mitad, independientemente de cuántas veces se use y qué día de la semana sea.
- Escribir un test que valide que una tarjeta de FranquiciaCompleta siempre puede pagar un boleto.
- Escribir un test que valide que el monto del boleto pagado con medio boleto es siempre la mitad del normal.
 
## Iteración 3
 
Al igual que la iteración anterior, se pide mantener la mecánica de trabajo para ir añadiendo las nuevas funcionalidades y/o modificaciones (issue, una rama específica para cada tarea y finalmente el merge cuando todo funcione correctamente..., etc.)
 
En esta iteración daremos una introducción a la manipulación de fechas y horarios. Éstos serán necesarios en esta oportunidad para realizar las modificaciones pedidas.
 
### Más datos sobre el boleto
 
La clase boleto tendrá nuevos métodos que permitan conocer: Fecha, tipo de tarjeta, línea de colectivo, total abonado, saldo e ID de la tarjeta. El boleto deberá indicar además el saldo restante en la tarjeta.
 
Además, el boleto deberá informar el monto total abonado en caso de que la tarjeta tuviera saldo negativo y eso produzca un valor final superior al valor normal de la tarifa.
 
Escribir los tests correspondientes a los posibles tipos de boletos a obtener según el tipo de tarjeta.
 
### Limitación en el pago de medio boletos
 
Para evitar el uso de una tarjeta de tipo medio boleto en más de una persona en el mismo viaje se pide que:
 
- Al utilizar una tarjeta de tipo medio boleto para viajar, deben pasar como mínimo 5 minutos antes de realizar otro viaje. No será posible pagar otro viaje antes de que pasen estos 5 minutos.
- Escribir un test que verifique efectivamente que no se deje marcar nuevamente al intentar realizar otro viaje en un intervalo menor a 5 minutos con la misma tarjeta medio boleto. Para el caso del medio boleto, se pueden realizar hasta dos viajes por día. El tercer viaje ya posee su valor normal.
- Escribir un test que verifique que no se puedan realizar más de dos viajes por día con medio boleto.
### Limitación en el pago de franquicias completas
 
Para evitar el uso de una tarjeta de tipo boleto educativo gratuito en más de una persona en el mismo viaje se pide que:
 
- Al utilizar una tarjeta de tipo boleto educativo gratuito se pueden realizar hasta dos viajes gratis por día.
- Escribir un test que verifique que no se puedan realizar más de dos viajes gratuitos por día.
- Escribir un test que verifique que los viajes posteriores al segundo se cobran con el precio completo.
### Saldo de la tarjeta
 
Una tarjeta SUBE no puede almacenar más de 56000 pesos. Por lo tanto, cuando se realiza una carga que haga que se supere este límite, se deberá acreditar la carga en la tarjeta hasta alcanzar el monto máximo permitido y el monto restante se deberá dejar pendiente de acreditación. Luego ese saldo pendiente se acredita a medida que se usa la tarjeta.
 
- Crear el método `AcreditarCarga`.
- Modificar la función para cargar la tarjeta añadiendo esta funcionalidad.
- Escribir un test que valide que si a una tarjeta se le carga un monto que supere el máximo permitido, se acredite el saldo hasta alcanzar el máximo (56000) y que el excedente quede almacenado y pendiente de acreditación.
- Escribir un test que valide que luego de realizar un viaje, verifique si hay saldo pendiente de acreditación y recargue la tarjeta hasta llegar al máximo nuevamente.

## Iteración 4

### Actualización de tarifas

La tarifa básica de un pasaje aumentó: pasa de $1580 a **$2150**. Actualizar el valor en el código y revisar que todos los tests sigan funcionando con la nueva tarifa.

### Boleto de uso frecuente

Las tarjetas SUBE cuentan con el boleto de uso frecuente. Este es un beneficio que aplica un descuento al monto del boleto dependiendo de cuántos viajes se hagan.

- Del viaje 1 al 29: Tarifa normal.
- Del viaje 30 al 79: 20% de descuento.
- Del viaje 80 en adelante: 25% de descuento.

La cantidad de viajes se cuenta del primer al último día de cada mes. Este beneficio se aplicará *sólo* sobre las tarjetas de tipo Normal.

- Implementar esta nueva funcionalidad.
- Escribir los tests correspondientes para validar el correcto funcionamiento del código.

### Franquicias

Todas las franquicias (todos los tipos de tarjeta distintos de Normal) solo pueden utilizarse en una determinada franja horaria:

- Lunes a viernes de 6 a 22.

Fuera de este intervalo de tiempo no es posible pagar con ninguna de estas franquicias.

- Escribir tests que validen que no se puedan realizar viajes fuera de la franja horaria y/o días correspondientes.

### Líneas interurbanas

En nuestra ciudad existen diversas líneas de colectivo. Algunas solo viajan dentro de la ciudad pero otras van hacia la zona metropolitana de Rosario (ej: Gálvez, Baigorria). Estas líneas tienen otra tarifa.

- Implementar las líneas de colectivo interurbanas.
- La tarifa interurbana es de: $3000.

Las líneas de colectivo interurbanas admiten todas las franquicias y siguen los mismos criterios que las líneas de colectivo urbanas.

### Trasbordos

Un usuario de la tarjeta SUBE puede realizar trasbordos entre colectivos sin costo de acuerdo a las siguientes condiciones:

- El usuario puede realizar trasbordos sin límite en un plazo de 1 hora desde que se pagó el primer boleto, pero el trasbordo debe ser entre líneas distintas.
- Los trasbordos se pueden realizar de lunes a sábado de 7:00 a 22:00.
- Todas las tarjetas pueden realizar trasbordos.
- Cuando se paga un trasbordo se indica también en el boleto.