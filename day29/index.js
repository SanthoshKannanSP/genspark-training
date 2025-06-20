// Callback, Promise, async/await

async function getUsersUsingCallback(callback)
{
    var data = await fetch('https://dummyjson.com/users')
    data = await data.json();
    data = await data.users;
    callback(data, "Callback");
}

function displayUsers(data, type)
{
    const dataDiv = document.getElementById("data");
    dataDiv.innerHTML = null
    var heading = document.createElement("h2");
    heading.innerHTML = type;
    dataDiv.appendChild(heading);

    data.forEach(element => {
        var child = document.createElement("p");
        child.innerHTML = element.firstName + " " + element.lastName;
        dataDiv.appendChild(child);
    });
}

function onClickCallback()
{
    getUsersUsingCallback(displayUsers);
}

function onClickPromise()
{
    const myPromise = new Promise((resolve, reject) => {
        fetch("https://dummyjson.com/users")
        .then(data => data.json())
        .then(data => resolve(data.users))
        .catch((err) => reject(err))
    });

    myPromise.then(data => displayUsers(data, "Promise"))
    .catch(err => console.log(err))
}

async function onClickAsyncAwait()
{
    var data = await fetch("https://dummyjson.com/users")
    data = await data.json();
    data = await data.users;
    displayUsers(data, "Async/Await");
}