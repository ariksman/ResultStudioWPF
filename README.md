## The program - ResultStudio
This implementation follows MVVM design principle and utilizes WPF for the user interface. Additionally, the code is divided into layers and utilises CQS for the use-case implementation.

Functionalities are the ability to create random measurement data of a subframe point with x,y,z measurements and optionally read data from a file. Datasets of 20 subframes is used as default. 

The variance of datapoint for x,y,z is calculated and data is visualised with the help of WPF datagrid and OxyPlot. For data analysing, user can set tolerances invidually for x,y,z measurements. If measurement value exceeds this limit, the cell containing invalid data is highlighted by red color.

## Frameworks

- Mvvm light [http://www.mvvmlight.net]
- OxyPlot [http://www.oxyplot.org]
- NUnit [http://nunit.org]
- Autofac [https://autofac.org/]
- AutoMapper [https://automapper.org]

#

*If debugging is the process of removing software bugs, then programming must be the process of putting them in.*
