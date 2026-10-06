# Trabajo Tarjeta 2026

## Integrantes del grupo
 - Apellido y nombre
 - Apellido y nombre

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
 