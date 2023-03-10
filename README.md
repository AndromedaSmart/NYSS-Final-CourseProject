# Итоговая курсовая работа C# от NYSS (компания First Line Software)

## ASP.NET Core Веб-Приложение
  <b>Функционал:</b>
  <br><i>Реализованный функционал соответствует продвинутому результату в рамках курсового проекта</i>
  <li>Шифрование и дешифрование текста методом Виженера (фр. Chiffre de Vigenère)) с использованием пользовательского ключа
  <li>Поддержка кириллицы и латиницы
  <li>Сохранение регистра исходного текста
  <li>Ввод текста в окне Веб-приложения, а также загрузка из текстовых файлов (.txt, .docx)
  <li>Загрузка результата (расшифрованной информации) в виде файла (.txt, .docx)
  <li>Основной функционал (шифрование и дешифрование) покрыт автоматическими Unit тестами с использованием NUnit
    
## Демонстрация работы приложения
https://user-images.githubusercontent.com/57660693/165960807-f2712445-25b7-40cb-ab7b-e89dd369450d.mp4

## Содержимое проекта
    
  ### Класс Cipher.cs
Содержит 2 основных метода программы - Encrypt() и Decrypt() и осуществляет шифрование/дешифрование с использованием "кастомных" словарей соответствия между символами алфавита и целочисленными эквивалентами

    Для шифрования используется сдвиг по алфавиту, равный значению соответствующего символа ключа:
    `AlphabetIntToChar[(AlphabetCharToInt[text[i]] + AlphabetCharToInt[keyword[i % len]]) % Power])`
    
    Для дешифрования используется обратный сдвиг по алфавиту, равный значению соответствующего символа ключа:  
    `AlphabetIntToChar[(AlphabetCharToInt[text[i]] - AlphabetCharToInt[keyword[i % len]] + Power) % Power])`
![Cipher.cs](https://user-images.githubusercontent.com/57660693/165961560-d9a7b38c-0d9d-4512-b8dd-8240bc94e09f.png)
    
  ### Файл главной страницы Index.cshtml
Содержит HTML структуру главной страницы и всех её элементов
![Index.cshtml](https://user-images.githubusercontent.com/57660693/165952629-a3772e26-71cf-41e5-bfa0-d08074d1b3cb.png)
    
  ### Файл "бэка" главной страницы Index.cshtml.cs
Содержит код на языке программирования C#, отвечающий за связь элементов пользовательского интерфейса с внутренней (серверной) логикой программы 
<li>Основной метод обработки POST запроса: принятие текста/шифротекста, осуществление шифровки/расшифровки в соответствии с выбранными параметрами (ключ, алфавит, функция) с последующей генерацией файлов (.txt, .docx), которые пользователь может загрузить

![Index.cshtml.cs](https://user-images.githubusercontent.com/57660693/165953728-0acd0239-490a-467b-966d-c1a32fede051.png)

<li>Основные методы работы с файлами: GenerateFiles() и FileParser()

![image](https://user-images.githubusercontent.com/57660693/165954155-0c4d2844-ca2b-428f-a58c-beb7ce456bc2.png)
  
  ### Класс Test.cs
Содержит код, используемый для осуществления автоматических Unit тестов с использованием NUnit Framework
  
![image](https://user-images.githubusercontent.com/57660693/165954770-a77aadbc-12ca-4e18-a75e-2017e94f4e56.png)
