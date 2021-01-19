import React from 'react';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import './CheckBoxStyle.css'


/*export default function CheckboxLabels(labelName) {
    const [state, setState] = React.useState({
        checkedA: true,
    });
    const handleChange = (event) => {
        setState({ ...state, [event.target.name]: event.target.checked });
    };

    return (
        <FormGroup row>
            <FormControlLabel
                control={<Checkbox className=checked={state.checkedA} onChange={handleChange} name="checkedA" />}
                label={<label>{`${labelName}`}</label>}
            />
        </FormGroup>
    );
}*/

function CheckBox(props) {
    const styles = {
        fontColor: "white" ,
        font: "Helvetica"

    }
    return (
        <FormGroup row >
            <FormControlLabel 
                control={<Checkbox name={props.name} id={ props.id}/>}
                label={props.label}
            />
        </FormGroup>
    );
}
export default CheckBox