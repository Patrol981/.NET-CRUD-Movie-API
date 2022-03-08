import postData from './utils/addData.js';
import fs from 'fs';
import ImportDirector from './models/ImportDirector.js';
import ImportMovie from './models/ImportMovie.js';
import ImportSerial from './models/ImportSerial.js';

const jsonString = fs.readFileSync('../output.json');
const jsonObject = JSON.parse(jsonString);

const directors = jsonObject.directors;
const movies = jsonObject.movies;
const serials = jsonObject.serials;

const url = "http://localhost:5000/api/Import";

main();

async function main() {
  console.log("importing json data to API...");
  await importDirectors();
  await importMovies();
  await importSerials();
  console.log("all data imported.");
}

async function importMovies() {
  for(let i=0; i<movies.length; i++) {
    const movie = new ImportMovie(
      movies[i].movieID,
      movies[i].productionYear,
      movies[i].directorID,
      movies[i].movieLength,
      movies[i].movieTitle
    );
    await postData(`${url}/ImportMovie`, movie);
  }
}

async function importDirectors() {
  for(let i=0; i<directors.length; i++) {
    const director = new ImportDirector(
      directors[i].directorID,
      directors[i].firstname,
      directors[i].lastname
    );
    await postData(`${url}/ImportDirector`, director);
  }
}

async function importSerials() {
  for(let i=0; i<serials.length; i++) {
    const serial = new ImportSerial(
      serials[i].serialID,
      serials[i].productionYear,
      serials[i].directorID,
      serials[i].serialEpisodes,
      serials[i].serialTitle
    );
    await postData(`${url}/ImportSerial`, serial);
  }
}