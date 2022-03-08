import fetch from 'cross-fetch';

async function postData(url, data) {
  const response = await fetch(url, {
    method: "POST",
    mode: 'cors',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(data)
  });
}

export default postData;