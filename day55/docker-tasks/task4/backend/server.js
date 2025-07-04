const express = require("express");
const app = express();
const port = 3000;

// Simple route to return a name
app.get("/", (req, res) => {
  res.send("Santhosh Kannan");
});

// Start the server
app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
});
