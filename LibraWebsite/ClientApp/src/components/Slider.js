import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Slider from '@material-ui/core/Slider';

/*const useStyles = makeStyles({
    root: {
        width: 180,
    },

});*/

function valuetext(value) {
    return `${value} + €`;
}

export default function ContinuousSlider() {
    //const classes = useStyles();

    return (
        <div className="slider">
            <Grid item xs>
                <Slider
                    defaultValue={2000}
                    getAriaValueText={valuetext}
                    aria-labelledby="discrete-slider"
                    valueLabelDisplay="auto"
                    min={200}
                    max={4000}
                />
            </Grid>
        </div>
    );
}

