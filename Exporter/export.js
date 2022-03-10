import 'dotenv/config';
import fetchData from './utils/fetchData.js';
import fs from 'fs';

console.log("exporting API data to json...");

let directors;
let movies;
let serials;

const url = `${process.env.URL}/api`;

directors = await fetchData(`${url}/Director`);
movies = await fetchData(`${url}/Movie`);
serials = await fetchData(`${url}/Serial`);

const jsonData = {
  directors,
  movies,
  serials
}

const jsonString = JSON.stringify(jsonData);

fs.writeFile('../output.json', jsonString, 'utf-8', (err) => {
  if(err != null) {
    console.log('error occured while exporting json');
    return console.log(err);
  }
  return;
})

console.log("json file saved.");