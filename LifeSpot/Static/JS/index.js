// Логирование сессии (объявлено через expression)
let sessionLog = function logSession(session) {
    // Вывод в консоль
    for (let result of session) {
        console.log(result)
    }
}
function filterContent(inputParseFunction) { // в этот параметр будет передана ФУНКЦИЯ парсинга пользовательского ввода

    let elements = document.getElementsByClassName('video-container');

    for (let i = 0; i <= elements.length; i++) {
        let videoText = elements[i].querySelector(".video-title").innerText;
        if (!videoText.toLowerCase().includes(inputParseFunction() /*Переданная функция вызвана*/.toLowerCase())) {
            elements[i].style.display = 'none';
        } else {
            elements[i].style.display = 'inline-block';
        }
    }
}

function handleSession() {
    let session =  new Map();
    session.set("userAgent", window.navigator.userAgent)
    session.set("age", prompt("Пожалуйста, введите ваш возраст?"))
  
    if(session.get("age") >= 18){
        let startDate = new Date().toLocaleString();
        alert("Приветствуем на LifeSpot! " + '\n' +  "Текущее время: " + startDate );
        session.set("startDate", startDate)
    }
    else{
        alert("Наши трансляции не предназначены для лиц моложе 18 лет. ВыL будете перенаправлены");
        window.location.href = "http://www.google.com"
    }
    return session;
    }
 
  
