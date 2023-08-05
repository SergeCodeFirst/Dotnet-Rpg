import './App.css';
import React, { useState } from 'react';
import axios from 'axios';


function App() {
  const [allUsers, setAllUsers] = useState([]);

  const GetAllUser = () =>{
    axios.get("https://localhost:7255/api/Character/all")
      .then(res => {
        console.log(res);
      })
      .catch(err => {
        console.log(err);
      })
  }

  return (
    <div className="App">
      <h1>Hello</h1>
      <button onClick={GetAllUser}>Get User</button>
    </div>
  );
}

export default App;
