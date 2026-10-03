# TutorPack.UI

Razor Class Library с общими Material Design-компонентами и статическими
ресурсами Tutor Pack. Пакет распространяется через GitHub Packages для Blazor-приложений на .NET 10.

## Установка

```sh
dotnet add package TutorPack.UI --version 1.0.0
```

Добавьте namespace компонентов:

```razor
@using TutorPack.UI
```

Подключите общие стили и browser helpers:

```html
<link rel="stylesheet" href="_content/TutorPack.UI/controls.css" />
<script src="_content/TutorPack.UI/controls.js"></script>
```

В пакет входят `ActionMenu`, `AppIcon`, `MaterialDialog`, `MaterialInput`,
`MaterialSearch`, `MaterialSelect`, `MaterialSwitch` и `MaterialTabs`. Scoped CSS
и ресурсы из `wwwroot` доставляются стандартным механизмом static web assets.

Основной код продукта находится в репозитории
[`TutorPack/tutor-pack`](https://github.com/TutorPack/tutor-pack), документация —
в [`TutorPack/tutor-pack-docs`](https://github.com/TutorPack/tutor-pack-docs).

## Сборка

```sh
dotnet restore TutorPack.UI.slnx
dotnet build TutorPack.UI.slnx --configuration Release --no-restore
dotnet pack src/TutorPack.UI/TutorPack.UI.csproj --configuration Release --no-build --output artifacts
```

Новая версия публикуется в GitHub Packages после изменения `Version` и создания тега
`v<версия>`. Публикация использует `GITHUB_TOKEN` репозитория. Для установки пакета
добавьте источник `https://nuget.pkg.github.com/TutorPack/index.json` и передайте
учётные данные GitHub Packages через `NuGetPackageSourceCredentials_tutorpack`;
публиковать токен в `NuGet.Config` не нужно.
