import React, { useState, useEffect } from 'react';


export default function TitlebarGridList({ category, id }) {
    const [items, setItems] = useState([])

    useEffect(() => {
        let mounted = true;
        FetchAPI(category, id)
            .then(data => {
                if (mounted) {
                    setItems(data);
                }
            })
        console.log(items);
        return () => mounted = false;
    }, [])

    return (
        <div>
            <center>
                <div>
                    {items.map((review) => (
                        <div> {review.message} {review.score} </div>
                    ))}
            </div>
            </center>
        </div>
    );
}

function FetchAPI(category, id) {
    var proxyUrl = 'https://cors-anywhere.herokuapp.com/',
        targetUrl = '' + id + '/' + category 
    return fetch(proxyUrl + targetUrl).then(response => response.json());
}