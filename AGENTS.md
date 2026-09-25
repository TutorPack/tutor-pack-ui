# TutorPack.UI: инструкции для агентов

Эти правила действуют для всего репозитория.

- `TutorPack.UI` — публичный NuGet package и Razor Class Library для .NET 10.
  Сохраняй namespace, имена компонентов, параметры и пути `_content/TutorPack.UI`
  совместимыми. Перед изменением публичного API проверяй потребителей в
  [`TutorPack/tutor-pack`](https://github.com/TutorPack/tutor-pack).
- Следуй Material Design 3 и существующей визуальной системе Tutor Pack. Проверяй
  desktop и mobile, keyboard navigation, видимый focus, достаточный contrast,
  осмысленные `aria`-атрибуты и touch targets не меньше 48 CSS pixels.
- Общие browser helpers размещай в `wwwroot/controls.js`, общие токены и базовые
  стили — в `wwwroot/controls.css`, стили отдельного компонента — в его
  `.razor.css`.
- Перед изменениями проверяй `git status`. Считай существующие staged, modified и
  untracked файлы пользовательскими; сохраняй их и не откатывай.
- Для новой работы создавай feature-ветку с префиксом `codex/`. Не отправляй
  ветку и не создавай PR, пока пользователь этого не попросил.
- После изменений выполняй Release build и `dotnet pack`; для UI-изменений также
  проверяй затронутые компоненты в приложениях-потребителях через Docker Compose.
