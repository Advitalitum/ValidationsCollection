[![Build Status](https://dev.azure.com/Bince1949/Bince1949/_apis/build/status/Advitalitum.ValidationsCollection?branchName=master)](https://dev.azure.com/Bince1949/Bince1949/_build/latest?definitionId=1&branchName=master)
# Коллекция простых валидаций на C#

## Установка

### NuGet [https://www.nuget.org/packages/ValidationsCollection/](https://www.nuget.org/packages/ValidationsCollection/)
    
    PM> Install-Package ValidationsCollection
    
## Использование
Все методы объявлены в статическом классе ```Validations```
- Быстрая валидация ИНН
```csharp
  public static bool IsValidInn(string? innString)
```
```csharp
  public static bool IsValidInnForIndividual(string? innString)
```
```csharp
  public static bool IsValidInnForEntity(string? innString)
```
```csharp
  public static ref readonly InnValidationResult ValidateInn(string? innString)
```
- Быстрая валидация КПП
```csharp
  public static bool IsValidKpp(string? kppString)
```
- Быстрая валидация СНИЛС
```csharp
  public static bool IsValidSnils(string? snilsString)
```
