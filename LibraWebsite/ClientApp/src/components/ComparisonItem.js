import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import Button from '@material-ui/core/Button';
import { useHistory } from 'react-router';
import { useCookies, Cookies } from 'react-cookie';
import './ComparisonTab.css';
import logo from './img/libra500.png';


const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'space-around',
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    margin: {
        margin: theme.spacing(1),
    },
    gridList: {
        width: '100%',
        float: 'left',
        minHeight: '400px',
        minWidth: '464px',
        overflow: 'hidden',
        transform: 'translateZ(0)',
        marginLeft: 'auto',
        marginRight: 'auto',
    },
    photo: {
        display: 'flex',
        height: '100%',
        width: 'auto',
        alignItems: 'center',
        justifyContent: 'center',
        marginLeft: 'auto',
        marginRight: 'auto',
    },
    icon: {
        color: 'rgba(255, 255, 255, 0.54)',
    },

}));

export default function TitlebarGridList({ item, colorColumn }) {
    const classes = useStyles();
    const [cookies, setCookie, removeCookie] = useCookies(['']);
    const [tile, setTile] = useState(null);


    useEffect(() => {
        if (item === 1) { setTile(cookies.Item1); }
        else if (item === 2) { setTile(cookies.Item2); }
        else if (item === 3) { setTile(cookies.selectedItem);}
    }, [])

    console.log(tile);
    return (<div>{(tile != null) ?
        <div>
            <br />
            <div>
                <GridList cols={1}>
                    <GridListTile key={tile.id}>
                        <img className={classes.photo} src={tile.imageLink} />
                        <GridListTileBar
                            title={tile.name}
                            subtitle={tile.price.toLocaleString("en-US", { style: "currency", currency: "EUR" })}
                        />
                    </GridListTile>
                </GridList>
                {SpecsFactory(tile, classes)}
            </div>
            <br />
            <center>
                {item !== 3 ?
                    <Button variant="outlined" style={{ color: colorColumn, borderColor: colorColumn }} onClick={() => {
                        if (item === 1) { removeCookie('Item1'); }
                        else if (item === 2) { removeCookie('Item2'); }
                        setTile(null);
                    }
                    }>
                        Remove
                    </Button> : null}
            </center>
        </div>
        : <div>
            <br /><br /><br /><br /><br /><br />
            <GridList cols={1}>
                <GridListTile key='img'>
                    <img style={{
                        width: 'auto', height: '90%',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        marginLeft: 'auto',
                        marginRight: 'auto',
                    }} src={logo} />
                    <GridListTileBar
                        title="Empty"
                        subtitle="Choose an item to be compared"
                    />
                </GridListTile>
            </GridList>
        </div>}
    </div>);
}

/*async function postAPI(item1, item2) {
    // Simple POST request with a JSON body using fetch
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify([item1, item2])

    };
    const response = await fetch('api/Comparison/ComputerComparison/5/5/5', requestOptions);
    const data = await response.json().then(data => { console.log(data) });
}*/

function SpecsFactory(item, classes) {
    switch (item.itemCategory) {
        case 2:
            return DesktopComputerSpecs(item);
        case 0:
            return LaptopComputerSpecs(item);
        case 1:
            return SmartphoneSpecs(item);
    }
}
function DesktopComputerSpecs(tile) {
    return (
        <List>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Manufacturer: "} />
                <ListItemText primary={(tile.manufacturerName != null) ? tile.manufacturerName : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Processor: "} />
                <ListItemText primary={(tile.processor != null) ? tile.processor.model : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Graphic card: "} />
                <ListItemText primary={(tile.graphicsCardName != null) ? tile.graphicsCardName : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Storage: "} />
                <ListItemText primary={(tile.storageCapacity != 0) ? (tile.storageCapacity + ' GB ') : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"RAM: "} />
                <ListItemText primary={(tile.ram != 0) ? (tile.ram + ' GB ' + ((tile.raM_type != null) ? ('(' + tile.raM_type + ')') : '')) : 'Not specified'} />
            </ListItem>
        </List>)
}
function LaptopComputerSpecs(tile) {
    return (
        <List>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Manufacturer: "} />
                <ListItemText primary={(tile.manufacturerName != null) ? tile.manufacturerName : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Processor: "} />
                <ListItemText primary={(tile.processor != null) ? tile.processor.model : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Graphic card: "} />
                <ListItemText primary={(tile.graphicsCardName != null) ? tile.graphicsCardName : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Storage: "} />
                <ListItemText primary={(tile.storageCapacity != 0) ? (tile.storageCapacity + ' GB ') : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"RAM: "} />
                <ListItemText primary={(tile.ram != 0) ? (tile.ram + ' GB ' + ((tile.raM_type != null) ? ('(' + tile.raM_type + ')') : '')) : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"}>
                <ListItemText className={"infoRow"} primary={"Resolution: "} />
                <ListItemText primary={(tile.resolution != null) ? (tile.resolution) : 'Not specified'} />
            </ListItem>
        </List>)
}
function SmartphoneSpecs(tile) {
    return (
        <List>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"Manufacturer: "} />
                <ListItemText primary={(tile.manufacturerName != null) ? tile.manufacturerName : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"Processor: "} />
                <ListItemText primary={(tile.processor != null) ? tile.processor : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"Screen diagonal: "} />
                <ListItemText primary={(tile.screenDiagonal != null) ? tile.screenDiagonal : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"Ram: "} />
                <ListItemText primary={(tile.ram != 0) ? (tile.ram + ' GB ') : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"Cameras: "} />
                <ListItemText primary={(tile.backCameras != null) ? (tile.backCameras + ((tile.frontCameras != null) ? (' | ' + tile.frontCameras) : '')) : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"resolution: "} />
                <ListItemText primary={(tile.resolution != null) ? (tile.resolution) : 'Not specified'} />
            </ListItem>
            <ListItem className={"infoPanel"} dense={true}>
                <ListItemText className={"infoRow"} primary={"Battery storage: "} />
                <ListItemText primary={(tile.batteryStorage != null) ? ((tile.batteryStorage) + ' mAh') : 'Not specified'} />
            </ListItem>
        </List>)
}