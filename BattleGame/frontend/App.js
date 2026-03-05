
import React, {useEffect,useState} from "react";

export default function App(){
 const [data,setData]=useState([]);

 useEffect(()=>{
 fetch("http://localhost:7071/api/getassetsbyplayer")
 .then(r=>r.json())
 .then(d=>setData(d));
 },[]);

 return(
 <div>
 <h2>Player Assets Report</h2>
 <table border="1">
 <thead>
 <tr>
 <th>No</th>
 <th>Player</th>
 <th>Level</th>
 <th>Age</th>
 <th>Asset</th>
 </tr>
 </thead>
 <tbody>
 {data.map((x,i)=>(
 <tr key={i}>
 <td>{i+1}</td>
 <td>{x.PlayerName}</td>
 <td>{x.Level}</td>
 <td>{x.Age}</td>
 <td>{x.AssetName}</td>
 </tr>
 ))}
 </tbody>
 </table>
 </div>
 );
}
