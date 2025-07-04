fetch("/api")
  .then((res) => res.text())
  .then((name) => {
    document.getElementById("greeting").textContent = `Hello, ${name}`;
  })
  .catch((err) => {
    console.error("Error:", err);
    document.getElementById("greeting").textContent = "Error loading name";
  });
