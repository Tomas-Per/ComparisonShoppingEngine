import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import GridList from '@material-ui/core/GridList';
import GridListTile from '@material-ui/core/GridListTile';
import GridListTileBar from '@material-ui/core/GridListTileBar';
import ListSubheader from '@material-ui/core/ListSubheader';
import IconButton from '@material-ui/core/IconButton';
import InfoIcon from '@material-ui/icons/Info';
import mockData from './mockData';

const useStyles = makeStyles((theme) => ({
    root: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'space-around',
        overflow: 'hidden',
        backgroundColor: theme.palette.background.paper,
    },
    gridList: {
        width: '80%',
        height: '100%',
        float: 'left',
        minHeight: '300px',
        minWidth: '464px',
        overflow: 'hidden',
        transform: 'translateZ(0)',
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
export default function TitlebarGridList() {
    const classes = useStyles();

    return (
        <div className={classes.root}>
            <GridList cellHeight={200} cellWidht={ 200} className={classes.gridList}>
                {mockData.map((tile) => (
                    <GridListTile key={tile.id}>
                        <img src={tile.img}  />
                        <GridListTileBar
                            title={tile.title}
                            price={tile.price.toLocaleString("en-US", { style: "currency", currency: "EUR" })}
                            actionIcon={
                                <IconButton className={classes.icon}>
                                    <InfoIcon />
                                </IconButton>
                            }
                            />
                    </GridListTile>
                ))}
            </GridList>
        </div>
    );
}
//price={tile.price.toLocaleString("en-US", { style: "currency", currency: "EUR" })}