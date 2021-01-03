import React, { useState, useEffect } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import IconButton from '@material-ui/core/IconButton';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import RateReviewIcon from '@material-ui/icons/RateReview';
import VerticalAlignCenterIcon from '@material-ui/icons/VerticalAlignCenter';
import FavoriteBorderIcon from '@material-ui/icons/FavoriteBorder';
import SearchIcon from '@material-ui/icons/Search';
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart';
import Button from '@material-ui/core/Button';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import NavigateBeforeIcon from '@material-ui/icons/NavigateBefore';
import { useHistory } from 'react-router';
import { useCookies, Cookies } from 'react-cookie';
import './Styles.css';
import { Divider } from '@material-ui/core';


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
    demo: {
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    container: {
        height: '50%',
        minHeight: '1px',
        width: '40%',
        float: 'bottom',
        overflow: 'hidden',
    },
    gridList: {
        width: '80%',
        float: 'left',
        minHeight: '400px',
        minWidth: '464px',
        overflow: 'hidden',
        transform: 'translateZ(0)',
    },
    photo: {
        display: 'flex',
        height: '90%',
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

/**
 * The example data is structured as follows:
 *
 * import image from 'path/to/image.jpg';
 * [etc...]
 *
 * const tileData = [
 *   {
 *     img: image,
 *     title: 'Image',
 *     author: 'author',
 *   },
 *   {
 *     [etc...]
 *   },
 * ];
 */
export default function TitlebarGridList({ category, page }) {
    const classes = useStyles();
    const history = useHistory();
    const [active, setActive] = useState(null);
    const [items, setItems] = useState([])   
    const [pageItems, setPageItems] = useState([]);
    const [searchInput, setSearchInput] = useState("");
    const [cookies, setCookie] = useCookies(['Item1']);
 
    useEffect(() => {
        let mounted = true;
        FetchAPI(category, page)
            .then(data => {
                if (mounted) {
                    setItems(data);
                    setPageItems(data.slice((page-1)*20, (page)*20));
                }
            })
        console.log(items);

        return () => mounted = false;
    }, [])

    const handleChange = (e) => {
        e.preventDefault();
        setSearchInput(e.target.value);
        if (searchInput.length > 0) {
            var searchResult = items.filter((i) => {
                return i.name.toLowerCase().match(searchInput.toLocaleLowerCase());
            })
            setPageItems(searchResult);
        }
        else {
            setPageItems(items.slice((parseInt(page) - 1) * 20, (parseInt(page)) * 20));
        }
    };
  

    if (pageItems != []) {

        return (
            <div style={{ width: '100%' }}>
                <center>
                    <input type="text" placeHolder="Search..." onChange={handleChange} value={searchInput} />
                </center>
                <br />
                <br />
            <div className={classes.root}>
                <GridList cellHeight={200} cellWidht={200} className={classes.gridList}>
                    {pageItems.map((tile) => (
                        <GridListTile key={tile.id}>
                            <img className={classes.photo} src={tile.imageLink} />
                                <GridListTileBar className={(active == tile.id) ? "extract" : ''}
                                    title={tile.name}
                                    subtitle={tile.price.toLocaleString("en-US", { style: "currency", currency: "EUR" })}
                                    actionIcon={
                                        <IconButton className={classes.icon} onClick={() => {
                                            if (active === tile.id) {
                                                setActive(null);
                                            } else {
                                                setActive(tile.id);
                                            }
                                        }}>
                                            <ExpandMoreIcon />
                                        </IconButton>
                                    }
                            />
                            {SpecsFactory(category, tile, active, classes)}
                           
                        </GridListTile>
                    ))}
                </GridList>
                </div>
                <br/><br/><br/>
                <center>
                    <Button className={classes.margin} variant="outlined" color="secondary" onClick={() => { history.push("/products/" + category + "/1"); setPageItems(items.slice(0, 20)); }}>
                        1
                    </Button>
                    <Button color="secondary">...</Button>

                    {(parseInt(page) - 3 > 0) ?
                        <Button className={classes.margin} variant="outlined" color="secondary" onClick={() => { history.push("/products/" + category + "/" + (parseInt(page) - 1)); setPageItems(items.slice((parseInt(page) - 2) * 20, (parseInt(page) - 1) * 20)); }}>
                            {parseInt(page) - 3}
                        </Button> : null}
                    {(parseInt(page) - 2 > 0) ?
                        <Button className={classes.margin}  variant="outlined" color="secondary" onClick={() => { history.push("/products/" + category + "/" + (parseInt(page) - 1)); setPageItems(items.slice((parseInt(page) - 2) * 20, (parseInt(page) - 1) * 20)); }}>
                            {parseInt(page) - 2}
                        </Button> : null}
                    {(parseInt(page) - 1 !== 0) ?
                        <Button className={classes.margin} variant="outlined" size="large" color="secondary" startIcon={<NavigateBeforeIcon />} onClick={() => { history.push("/products/" + category + "/" + (parseInt(page) - 1)); setPageItems(items.slice((parseInt(page) - 2) * 20, (parseInt(page) - 1) * 20)); }}>
                            Previous Page
                        </Button> : null}

                    <Button color="secondary" size="large">{page}</Button>

                    {(parseInt(page) + 1 <= items.length / 20 + 1) ?
                        <Button className={classes.margin} variant="outlined" size="large" color="secondary" endIcon={<NavigateNextIcon />} onClick={() => { history.push("/products/" + category + "/" + (parseInt(page) + 1)); setPageItems(items.slice((parseInt(page)) * 20, (parseInt(page) + 1) * 20)); }}>
                            Next Page
                         </Button> : null}
                    {(parseInt(page) + 2 <= items.length / 20 + 1) ?
                        <Button className={classes.margin} variant="outlined" color="secondary" onClick={() => { history.push("/products/" + category + "/" + (parseInt(page) + 1)); setPageItems(items.slice((parseInt(page)) * 20, (parseInt(page) + 1) * 20)); }}>
                            {parseInt(page) + 2}
                        </Button> : null}
                    {(parseInt(page) + 3 <= items.length / 20 + 1) ?
                        <Button className={classes.margin} variant="outlined" color="secondary" onClick={() => { history.push("/products/" + category + "/" + (parseInt(page) + 1)); setPageItems(items.slice((parseInt(page)) * 20, (parseInt(page) + 1) * 20)); }}>
                            {parseInt(page) + 3}
                        </Button> : null}

                    <Button color="secondary">...</Button>
                    <Button className={classes.margin} variant="outlined" color="secondary" onClick={() => { history.push("/products/" + category + "/1"); setPageItems(items.slice(Math.round(items.length / 20))); }}>
                        {Math.round(items.length / 20)}
                    </Button>
                    
                </center>
                <br /><br /><br />
            </div>
        );
    }
    else {
        return null;
    }


    function SpecsFactory(category, item, active, classes) {
        switch (category) {
            case "Desktops":
                return ComputerSpecs(item, active, classes);
            case "Laptops":
                return ComputerSpecs(item, active, classes);
            case "Smartphones":
                return SmartphoneSpecs(item, active, classes);
        }
    }
    function ComputerSpecs(tile, active, classes) {
        return (
            <List className={(active == tile.id) ? "extractInfoPanel" : 'specs'}>
                <Divider />
                <ListItem className={"infoPanel"} dense={true}>
                    <ListItemText className={"infoRow"} primary={"Manufacturer: "} />
                    <ListItemText primary={(tile.manufacturerName != null) ? tile.manufacturerName : 'Not specified'} />
                </ListItem>
                <ListItem className={"infoPanel"} dense={true}>
                    <ListItemText className={"infoRow"} primary={"Processor: "} />
                    <ListItemText primary={(tile.processor != null) ? tile.processor.model : 'Not specified'} />
                </ListItem>
                <ListItem className={"infoPanel"} dense={true}>
                    <ListItemText className={"infoRow"} primary={"Graphic card: "} />
                    <ListItemText primary={(tile.graphicsCardName != null) ? tile.graphicsCardName : 'Not specified'} />
                </ListItem>
                <ListItem className={"infoPanel"} dense={true}>
                    <ListItemText className={"infoRow"} primary={"Storage: "} />
                    <ListItemText primary={(tile.storageCapacity != 0) ? (tile.storageCapacity + ' GB ') : 'Not specified'} />
                </ListItem>
                <ListItem className={"infoPanel"} dense={true}>
                    <ListItemText className={"infoRow"} primary={"RAM: "} />
                    <ListItemText primary={(tile.ram != 0) ? (tile.ram + ' GB ' + ((tile.raM_type != null) ? ('(' + tile.raM_type + ')') : '')) : 'Not specified'} />
                </ListItem>
                <ListItem className={"infoPanel"} dense={true}>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }}> <RateReviewIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ transform: "rotate(90deg)", marginLeft: "auto", marginRight: "auto" }} onClick={() => {
                        if (cookies.Item1 == null) {
                            setCookie('Item1', JSON.stringify(tile), { path: '/' });
                        }
                        else if (cookies.Item2 == null) {
                            setCookie('Item2', JSON.stringify(tile), { path: '/' });
                        }
                    }
                    }> <VerticalAlignCenterIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }}> <SearchIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }} > <FavoriteBorderIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }} onClick={() => { window.open(tile.itemURL, '_blank'); }}> <ShoppingCartIcon /> </IconButton>
                </ListItem>
            </List>)
    }
    function SmartphoneSpecs(tile, active, classes) {
        return (
            <List className={(active == tile.id) ? "extractInfoPanel" : 'specs'}>
                <Divider />
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
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }}> <RateReviewIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ transform: "rotate(90deg)", marginLeft: "auto", marginRight: "auto" }} onClick={() => {
                        if (cookies.Item1 == null) {
                            setCookie('Item1', JSON.stringify(tile), { path: '/' });
                        }
                        else if (cookies.Item2 == null) {
                            setCookie('Item2', JSON.stringify(tile), { path: '/' });
                        }
                    }
                    }> <VerticalAlignCenterIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }}> <SearchIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }} > <FavoriteBorderIcon /> </IconButton>
                    <IconButton className={classes.icon} style={{ marginLeft: "auto", marginRight: "auto" }} onClick={() => { window.open(tile.itemURL, '_blank'); }}> <ShoppingCartIcon /> </IconButton>
                </ListItem>
            </List>)
    }
}

function FetchAPI(category, page) {
    var proxyUrl = 'https://cors-anywhere.herokuapp.com/',
        targetUrl = '' + category + '/Page/0'
    return fetch(proxyUrl + targetUrl).then(response =>response.json());
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


//price={tile.price.toLocaleString("en-US", { style: "currency", currency: "EUR" })}