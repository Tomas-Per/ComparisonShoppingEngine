import React, { useState, useEffect } from 'react';
import Circle from 'react-circle';
import { useCookies } from 'react-cookie';
import NavigateNextIcon from '@material-ui/icons/NavigateNext';
import IconButton from '@material-ui/core/IconButton';
import NavigateBeforeIcon from '@material-ui/icons/NavigateBefore';

export default function TitlebarGridList(category) {
    const [cookies, setCookies, removeCookie] = useCookies(['']);
    const [weights, setWeights] = useState(null);
    const [ranking, setRanking] = useState(null);
    const colorItem1 = "rgb(229, 161, 219)";
    const colorItem2 = "rgb(128, 222, 234)";


    useEffect(() => {
        setWeights(cookies.comparisonWeight);
        if (cookies.Item1 != null) {
            if (cookies.Item2 != null) {
                postAPI(cookies.Item1, cookies.Item2, cookies.comparisonWeight);
            }

        }
    }, [])
    console.log(ranking);
    return (
    <div>
            {(cookies.Item1 != null && cookies.Item2 != null) ? CategoryFactory() : null }
        </div>
    );


    async function postAPI(item1, item2, compWeights) {
        // Simple POST request with a JSON body using fetch
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify([item1, item2])
        };
        switch (item1.itemCategory) {
            case 0:
                const response1 = await fetch('api/Comparison/ComputerComparison/' + compWeights[0] + '/' + compWeights[1] + '/' + compWeights[2], requestOptions);
                await response1.json().then(data => { setRanking(data) });
                break;
            case 1:
                console.log(JSON.stringify([item1, item2]));
                const response2 = await fetch('api/Comparison/SmartphoneComparison/' + compWeights[0] + '/' + compWeights[1] + '/' + compWeights[2] + '/' + compWeights[3], requestOptions);
                await response2.json().then(data => { setRanking(data) });
                break;
            case 2:
                const response3 = await fetch('api/Comparison/ComputerComparison/' + compWeights[0] + '/' + compWeights[1] + '/' + compWeights[2], requestOptions);
                await response3.json().then(data => { setRanking(data) });
                break;
        }

    }

    function CategoryFactory() {
        switch(cookies.Item1.itemCategory){
        case 0:
                return ComputerRankings();
                break;
            case 1:
                return SmartphoneRankings();
                break;
            case 2:
                return ComputerRankings();
                break;
        }
    }
    function SmartphoneRankings() {
        return (
            <div>
                <center> Overall </center>
                <div><center> <Circle
                    animate={true} // Boolean: Animated/Static progress
                    animationDuration="2s" //String: Length of animation
                    responsive={false} // Boolean: Make SVG adapt to parent size
                    size={130} // Number: Defines the size of the circle.
                    lineWidth={20} // Number: Defines the thickness of the circle's stroke.
                    progress={(ranking !== null) ? ((ranking.itemRanking1 > ranking.itemRanking2) ? ranking.itemRanking1 : ranking.itemRanking2) : 0} // Number: Update to change the progress and percentage.
                    progressColor={(ranking !== null) ? ((ranking.itemRanking1 > ranking.itemRanking2) ? colorItem1 : colorItem2) : 'white'}  // String: Color of "progress" portion of circle.
                    bgColor={(ranking !== null) ? ((ranking.itemRanking1 < ranking.itemRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of "empty" portion of circle.
                    textColor={(ranking !== null) ? ((ranking.itemRanking1 > ranking.itemRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of percentage text color.
                    textStyle={{
                        font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                    }}
                    percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                    roundedStroke={true} // Boolean: Rounded/Flat line ends
                    showPercentage={true} // Boolean: Show/hide percentage.
                    showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                />
                    <div>
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [(weights[0] - 1), weights[1], weights[2], weights[3]])
                            setCookies('comparisonWeight', JSON.stringify([(weights[0] - 1), weights[1], weights[2], weights[3]], { path: '/' }));
                            setWeights([(weights[0] - 1), weights[1], weights[2], weights[3]]);
                        }}>
                            <NavigateBeforeIcon fontSize="small" />
                        </IconButton>
                        Price: {(weights !== null) ? weights[0] : ''}
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [(weights[0] + 1), weights[1], weights[2], weights[3]])
                            setCookies('comparisonWeight', JSON.stringify([(weights[0] + 1), weights[1], weights[2], weights[3]], { path: '/' }));
                            setWeights([(weights[0] + 1), weights[1], weights[2], weights[3]]);
                        }}>
                            <NavigateNextIcon fontSize="small" />
                        </IconButton>
                    </div>
                    <Circle
                        animate={true} // Boolean: Animated/Static progress
                        animationDuration="2s" //String: Length of animation
                        size={65} // Number: Defines the size of the circle.
                        lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                        progress={(ranking !== null) ? ((ranking.priceRanking1 > ranking.priceRanking2) ? Math.round(ranking.priceRanking1 * 10000 / (ranking.priceRanking1 + ranking.priceRanking2)) / 100 : Math.round(ranking.priceRanking2 * 10000 / (ranking.priceRanking1 + ranking.priceRanking2)) / 100) : 0} // Number: Update to change the progress and percentage.
                        progressColor={(ranking !== null) ? ((ranking.priceRanking1 > ranking.priceRanking2) ? colorItem1 : colorItem2) : 'white'}  // String: Color of "progress" portion of circle.
                        bgColor={(ranking !== null) ? ((ranking.priceRanking1 < ranking.priceRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of "empty" portion of circle.
                        textColor={(ranking !== null) ? ((ranking.priceRanking1 > ranking.priceRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of percentage text color.
                        textStyle={{
                            font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                        }}
                        percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                        roundedStroke={true} // Boolean: Rounded/Flat line ends
                        showPercentage={true} // Boolean: Show/hide percentage.
                        showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                    />
                    <div> <IconButton onClick={() => {
                        postAPI(cookies.Item1, cookies.Item2, [weights[0], (weights[1] - 1), weights[2], weights[3]])
                        setCookies('comparisonWeight', JSON.stringify([weights[0], (weights[1] - 1), weights[2], weights[3]], { path: '/' }));
                        setWeights([weights[0], (weights[1] - 1), weights[2], weights[3]]);
                    }}>
                        <NavigateBeforeIcon fontSize="small" />
                    </IconButton>
                        RAM: {(weights !== null) ? weights[1] : ''}
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [weights[0], (weights[1] + 1), weights[2], weights[3]])
                            setCookies('comparisonWeight', JSON.stringify([weights[0], (weights[1] + 1), weights[2], weights[3]], { path: '/' }));
                            setWeights([weights[0], (weights[1] + 1), weights[2], weights[3]]);
                        }}>
                            <NavigateNextIcon fontSize="small" />
                        </IconButton></div>
                    <Circle
                        animate={true} // Boolean: Animated/Static progress
                        animationDuration="2s" //String: Length of animation
                        size={65} // Number: Defines the size of the circle.
                        lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                        progress={(ranking !== null) ? ((ranking.ramRanking1 > ranking.ramRanking2) ? Math.round(ranking.ramRanking1 * 10000 / (ranking.ramRanking1 + ranking.ramRanking2)) / 100 : Math.round(ranking.ramRanking2 * 10000 / (ranking.ramRanking1 + ranking.ramRanking2)) / 100) : 0} // Number: Update to change the progress and percentage.
                        progressColor={(ranking !== null) ? ((ranking.ramRanking1 > ranking.ramRanking2) ? colorItem1 : colorItem2) : 'white'}  // String: Color of "progress" portion of circle.
                        bgColor={(ranking !== null) ? ((ranking.ramRanking1 <= ranking.ramRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of "empty" portion of circle.
                        textColor={(ranking !== null) ? ((ranking.ramRanking1 > ranking.ramRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of percentage text color.
                        textStyle={{
                            font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                        }}
                        percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                        roundedStroke={true} // Boolean: Rounded/Flat line ends
                        showPercentage={true} // Boolean: Show/hide percentage.
                        showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                    />
                    <div> <IconButton onClick={() => {
                        postAPI(cookies.Item1, cookies.Item2, [weights[0], weights[1], (weights[2] - 1), weights[3]])
                        setCookies('comparisonWeight', JSON.stringify([weights[0], weights[1], (weights[2] - 1), weights[3]], { path: '/' }));
                        setWeights([weights[0], weights[1], (weights[2] - 1), weights[3]]);
                    }}>
                        <NavigateBeforeIcon fontSize="small" />
                    </IconButton>
                        Storage: {(weights !== null) ? weights[2] : ''}
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [weights[0], weights[1], (weights[2] + 1), weights[3]])
                            setCookies('comparisonWeight', JSON.stringify([weights[0], weights[1], (weights[2] + 1), weights[3]], { path: '/' }));
                            setWeights([weights[0], weights[1], (weights[2] + 1), weights[3]]);
                        }}>
                            <NavigateNextIcon fontSize="small" />
                        </IconButton> </div>
                    <Circle
                        animate={true} // Boolean: Animated/Static progress
                        animationDuration="2s" //String: Length of animation
                        size={65} // Number: Defines the size of the circle.
                        lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                        progress={(ranking !== null) ? ((ranking.storageRanking1 > ranking.storageRanking2) ? Math.round(ranking.storageRanking1 * 10000 / (ranking.storageRanking1 + ranking.storageRanking2)) / 100 : Math.round(ranking.storageRanking2 * 10000 / (ranking.storageRanking1 + ranking.storageRanking2)) / 100) : 0} // Number: Update to change the progress and percentage.
                        progressColor={(ranking !== null) ? ((ranking.storageRanking1 > ranking.storageRanking2) ? colorItem1 : colorItem2) : 'white'}  // String: Color of "progress" portion of circle.
                        bgColor={(ranking !== null) ? ((ranking.storageRanking1 < ranking.storageRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of "empty" portion of circle.
                        textColor={(ranking !== null) ? ((ranking.storageRanking1 > ranking.storageRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of percentage text color.
                        textStyle={{
                            font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                        }}
                        percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                        roundedStroke={true} // Boolean: Rounded/Flat line ends
                        showPercentage={true} // Boolean: Show/hide percentage.
                        showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                    />
                <div> <IconButton onClick={() => {
                        postAPI(cookies.Item1, cookies.Item2, [weights[0], weights[1], weights[2], (weights[3] - 1)])
                        setCookies('comparisonWeight', JSON.stringify([weights[0], weights[1], weights[2], (weights[3] - 1)], { path: '/' }));
                        setWeights([weights[0], weights[1], weights[2], (weights[3] - 1)]);
                }}>
                    <NavigateBeforeIcon fontSize="small" />
                </IconButton>
                        Cameras: {(weights !== null) ? weights[3] : ''}
                    <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [weights[0], weights[1], weights[2], (weights[3] - 1)])
                            setCookies('comparisonWeight', JSON.stringify([weights[0], weights[1], weights[2], (weights[3] - 1)], { path: '/' }));
                            setWeights([weights[0], weights[1], weights[2], (weights[3] - 1)]);
                    }}>
                        <NavigateNextIcon fontSize="small" />
                    </IconButton> </div>
                <Circle
                    animate={true} // Boolean: Animated/Static progress
                    animationDuration="2s" //String: Length of animation
                    size={65} // Number: Defines the size of the circle.
                    lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                    progress={(ranking !== null) ? ((ranking.storageRanking1 > ranking.storageRanking2) ? Math.round(ranking.storageRanking1 * 10000 / (ranking.storageRanking1 + ranking.storageRanking2)) / 100 : Math.round(ranking.storageRanking2 * 10000 / (ranking.storageRanking1 + ranking.storageRanking2)) / 100) : 0} // Number: Update to change the progress and percentage.
                    progressColor={(ranking !== null) ? ((ranking.storageRanking1 > ranking.storageRanking2) ? colorItem1 : colorItem2) : 'white'}  // String: Color of "progress" portion of circle.
                    bgColor={(ranking !== null) ? ((ranking.storageRanking1 < ranking.storageRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of "empty" portion of circle.
                    textColor={(ranking !== null) ? ((ranking.storageRanking1 > ranking.storageRanking2) ? colorItem1 : colorItem2) : 'white'} // String: Color of percentage text color.
                    textStyle={{
                        font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                    }}
                    percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                    roundedStroke={true} // Boolean: Rounded/Flat line ends
                    showPercentage={true} // Boolean: Show/hide percentage.
                    showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                /></center>
               </div>

            </div>
        );
    }
    function ComputerRankings() {
        return (
            <div>
                <center> Overall </center>
                <div><center> <Circle
                    animate={true} // Boolean: Animated/Static progress
                    animationDuration="2s" //String: Length of animation
                    responsive={false} // Boolean: Make SVG adapt to parent size
                    size={200} // Number: Defines the size of the circle.
                    lineWidth={20} // Number: Defines the thickness of the circle's stroke.
                    progress={(ranking !== null) ? ((ranking.itemRanking1 > ranking.itemRanking2) ? ranking.itemRanking1 : ranking.itemRanking2) : 0} // Number: Update to change the progress and percentage.
                    progressColor={(ranking !== null) ? ((ranking.itemRanking1 > ranking.itemRanking2) ? colorItem1 : colorItem2) :'white'}  // String: Color of "progress" portion of circle.
                    bgColor={(ranking !== null) ?((ranking.itemRanking1 < ranking.itemRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of "empty" portion of circle.
                    textColor={(ranking !== null) ?((ranking.itemRanking1 > ranking.itemRanking2) ? colorItem1 : colorItem2): 'white'} // String: Color of percentage text color.
                    textStyle={{
                        font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                    }}
                    percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                    roundedStroke={true} // Boolean: Rounded/Flat line ends
                    showPercentage={true} // Boolean: Show/hide percentage.
                    showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                />
                    <div>
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [(weights[0] - 1), weights[1], weights[2]])
                            setCookies('comparisonWeight', JSON.stringify([(weights[0] - 1), weights[1], weights[2]], { path: '/' }));
                            setWeights([(weights[0] - 1), weights[1], weights[2]]);
                        }}>
                            <NavigateBeforeIcon fontSize="small" />
                        </IconButton>
                        Price: {(weights !== null)? weights[0]: ''}
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [(weights[0] + 1), weights[1], weights[2]])
                            setCookies('comparisonWeight', JSON.stringify([(weights[0] + 1), weights[1], weights[2]], { path: '/' }));
                            setWeights([(weights[0] + 1), weights[1], weights[2]]);
                        }}>
                            <NavigateNextIcon fontSize="small" />
                        </IconButton>
                    </div>
                    <Circle
                        animate={true} // Boolean: Animated/Static progress
                        animationDuration="2s" //String: Length of animation
                        size={100} // Number: Defines the size of the circle.
                        lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                        progress={(ranking !== null) ?((ranking.priceRanking1 > ranking.priceRanking2) ? Math.round(ranking.priceRanking1 * 10000 / (ranking.priceRanking1 + ranking.priceRanking2)) / 100 : Math.round(ranking.priceRanking2 * 10000 / (ranking.priceRanking1 + ranking.priceRanking2)) / 100) : 0} // Number: Update to change the progress and percentage.
                        progressColor={(ranking !== null) ?((ranking.priceRanking1 > ranking.priceRanking2) ? colorItem1 : colorItem2):'white'}  // String: Color of "progress" portion of circle.
                        bgColor={(ranking !== null) ?((ranking.priceRanking1 < ranking.priceRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of "empty" portion of circle.
                        textColor={(ranking !== null) ?((ranking.priceRanking1 > ranking.priceRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of percentage text color.
                        textStyle={{
                            font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                        }}
                        percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                        roundedStroke={true} // Boolean: Rounded/Flat line ends
                        showPercentage={true} // Boolean: Show/hide percentage.
                        showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                    />
                    <div> <IconButton onClick={() => {
                        postAPI(cookies.Item1, cookies.Item2, [weights[0], (weights[1] - 1), weights[2]])
                        setCookies('comparisonWeight', JSON.stringify([weights[0], (weights[1] - 1), weights[2]], { path: '/' }));
                        setWeights([weights[0], (weights[1] - 1), weights[2]]);
                    }}>
                        <NavigateBeforeIcon fontSize="small" />
                    </IconButton>
                        RAM: {(weights !== null) ? weights[1] : ''}
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [weights[0], (weights[1] + 1), weights[2]])
                            setCookies('comparisonWeight', JSON.stringify([weights[0], (weights[1] + 1), weights[2]], { path: '/' }));
                            setWeights([weights[0], (weights[1] + 1), weights[2]]);
                        }}>
                            <NavigateNextIcon fontSize="small" />
                        </IconButton></div>
                    <Circle
                        animate={true} // Boolean: Animated/Static progress
                        animationDuration="2s" //String: Length of animation
                        size={100} // Number: Defines the size of the circle.
                        lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                        progress={(ranking !== null) ?((ranking.ramRanking1 > ranking.ramRanking2) ? Math.round(ranking.ramRanking1 * 10000 / (ranking.ramRanking1 + ranking.ramRanking2)) / 100 : Math.round(ranking.ramRanking2 * 10000 / (ranking.ramRanking1 + ranking.ramRanking2)) / 100):0} // Number: Update to change the progress and percentage.
                        progressColor={(ranking !== null) ?((ranking.ramRanking1 > ranking.ramRanking2) ? colorItem1 : colorItem2):'white'}  // String: Color of "progress" portion of circle.
                        bgColor={(ranking !== null) ?((ranking.ramRanking1 <= ranking.ramRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of "empty" portion of circle.
                        textColor={(ranking !== null) ?((ranking.ramRanking1 > ranking.ramRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of percentage text color.
                        textStyle={{
                            font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                        }}
                        percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                        roundedStroke={true} // Boolean: Rounded/Flat line ends
                        showPercentage={true} // Boolean: Show/hide percentage.
                        showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                    />
                    <div> <IconButton onClick={() => {
                        postAPI(cookies.Item1, cookies.Item2, [weights[0], weights[1], (weights[2] - 1)])
                        setCookies('comparisonWeight', JSON.stringify([weights[0], weights[1], (weights[2] - 1)], { path: '/' }));
                        setWeights([weights[0], weights[1], (weights[2] - 1)]);
                    }}>
                        <NavigateBeforeIcon fontSize="small" />
                    </IconButton>
                        Storage: {(weights !== null) ? weights[2] : ''}
                        <IconButton onClick={() => {
                            postAPI(cookies.Item1, cookies.Item2, [weights[0], weights[1], (weights[2] + 1)])
                            setCookies('comparisonWeight', JSON.stringify([weights[0], weights[1], (weights[2] + 1)], { path: '/' }));
                            setWeights([weights[0], weights[1], (weights[2] + 1)]);
                        }}>
                            <NavigateNextIcon fontSize="small" />
                        </IconButton> </div>
                    <Circle
                        animate={true} // Boolean: Animated/Static progress
                        animationDuration="2s" //String: Length of animation
                        size={100} // Number: Defines the size of the circle.
                        lineWidth={15} // Number: Defines the thickness of the circle's stroke.
                        progress={(ranking !== null) ?((ranking.storageRanking1 > ranking.storageRanking2) ? Math.round(ranking.storageRanking1 * 10000 / (ranking.storageRanking1 + ranking.storageRanking2)) / 100 : Math.round(ranking.storageRanking2 * 10000 / (ranking.storageRanking1 + ranking.storageRanking2)) / 100) : 0} // Number: Update to change the progress and percentage.
                        progressColor={(ranking !== null) ?((ranking.storageRanking1 > ranking.storageRanking2) ? colorItem1 : colorItem2):'white'}  // String: Color of "progress" portion of circle.
                        bgColor={(ranking !== null) ?((ranking.storageRanking1 < ranking.storageRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of "empty" portion of circle.
                        textColor={(ranking !== null) ?((ranking.storageRanking1 > ranking.storageRanking2) ? colorItem1 : colorItem2):'white'} // String: Color of percentage text color.
                        textStyle={{
                            font: 'bold 5rem Helvetica, Arial, sans-serif' // CSSProperties: Custom styling for percentage.
                        }}
                        percentSpacing={10} // Number: Adjust spacing of "%" symbol and number.
                        roundedStroke={true} // Boolean: Rounded/Flat line ends
                        showPercentage={true} // Boolean: Show/hide percentage.
                        showPercentageSymbol={true} // Boolean: Show/hide only the "%" symbol.
                    /></center>
                </div>

            </div>
            );
    }
}