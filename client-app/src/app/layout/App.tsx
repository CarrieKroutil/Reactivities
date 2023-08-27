import React, { Fragment, useEffect, useState } from 'react';
import axios from 'axios';
import { Button, Container, Header, List } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';


function App() {
  // Hook: useState returns a stateful value, and a function to update it. 
  // More: https://react.dev/reference/react/useState
  const [activities, setActivities] = useState<Activity[]>([]);
  // Note, the above [] inside useState gives our activities prop a default value of an empty list.

  // In order to cause a side effect, when app/component is initiaized, need to tell app what to do when loading up. 
  // More: https://react.dev/reference/react/useEffect
  useEffect(() => {
    // All HTTP methods are available through axios.
    // The get returns a promise, which we need to tell it what to do by chaining on the "then" method,
    //  to put the response back from API into activities state variable.

    // Side example what fetch looks like instead of using Axios:
    // fetch("http://localhost:5000/api/activities").then(async response => {
    //   console.log(response);
    //  setActivities(await response.json());

    axios.get<Activity[]>("http://localhost:5000/api/activities").then(response => {
      //console.log(response);
      setActivities(response.data);
    })
      .catch(function (error) {

        if (error.response) {
          // The request was made and the server responded with a status code
          // that falls out of the range of 2xx
          console.log(error.response.data);
          console.log(error.response.status);
          console.log(error.response.headers);
        } else if (error.request) {
          // The request was made but no response was received
          // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
          // http.ClientRequest in node.js
          console.log(error.request);
        } else {
          // Something happened in setting up the request that triggered an Error
          console.log('Error', error.message);
        }
        console.log(error.config);

      })
      .finally(function () {
        // always executed
      });
  }, []);
  // Note, the above [] empty list dependancy was used to not rerun effect indefinately and only if the list changes.

  return (
    // Only allowed to return a single element per React component, so using <Fragment> to group the NavBar and Container into one element.
    // Note: an empty <> element is the shortcut for fragment.
    <>
      <NavBar />
      <Container style={{marginTop: '7em'}}>
        <List>
          {activities.map(activity => (
            <List.Item key={activity.id}>
              {activity.title}
            </List.Item>
          ))}
        </List>
      </Container>

    </>
  );
  // Note, because activity is not an interface, we needed to define it as any type to resolve compile errors, but is not type safe.
}

export default App;
