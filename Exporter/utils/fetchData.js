import fetch from 'cross-fetch';

async function fetchData(url) {
  const response = await fetch(url, {
    method: "GET",
  });
  return await response.json();
}

export default fetchData;