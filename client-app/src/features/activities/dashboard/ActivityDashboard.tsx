import React from "react";
import { Grid, List } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityList from "./ActivityList";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";

interface Props {
    activities: Activity[];
    selectedActivity: Activity | undefined;
    selectActivity: (id: string) => void;
    cancelSelectActivity: () => void;
    editMode: boolean;
    openForm: (id: string) => void;
    closeForm: () => void;
  }

// Could pass in props: Props and then everything would need to reference props (E.g. props.activities) first, or using {activities}: Props will destructure the property passed in
export default function ActivityDashboard({activities, selectedActivity,
    selectActivity, cancelSelectActivity, editMode, openForm, closeForm}: Props) {
    // SemanticUI has a 16 column grid (not 12 like other libs)
    return (
        <Grid>
            <Grid.Column width='10'>
                <ActivityList activities={activities} selectActivity={selectActivity}></ActivityList>
            </Grid.Column>
        <Grid.Column width='6'>
            {selectedActivity && !editMode &&
            <ActivityDetails 
                activity={selectedActivity} 
                cancelSelectActivity={cancelSelectActivity}
                openForm={openForm} />}
            {editMode &&
            <ActivityForm closeForm={closeForm} activity={selectedActivity} />}
        </Grid.Column>
        </Grid>
    )
}