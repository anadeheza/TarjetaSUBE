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
