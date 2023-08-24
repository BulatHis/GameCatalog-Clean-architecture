import Card from '@mui/material/Card';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Button from '@mui/material/Button';
import {Link} from 'react-router-dom';
import {useParams} from 'react-router-dom';
import React, {useEffect, useState} from 'react';
import {Paper} from "@mui/material";


export default function Game() {
    const {title} = useParams();
    const [gameData, setGameData] = useState(null);
    const [reviews, setReviews] = useState([]);
    const [adminReviews, setAdminReviews] = useState([]);
    useEffect(() => {
        console.log(localStorage.getItem('accessToken'))
        const accessToken = localStorage.getItem('accessToken');
        setIsLoggedIn(accessToken !== 'null' ? true : false);
    }, []);
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    useEffect(() => {
        fetch(`http://localhost:4044/getgamebytitle?title=${title}`, {
            method: 'GET', mode: 'cors', headers: {
                'Content-Type': 'application/json',
            },
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Ошибка при получении данных с сервера');
                }
            })
            .then((data) => {
                setGameData(data);
            })
            .catch((error) => {
                console.error('Ошибка при получении данных с сервера', error);
            });
    }, [title]);


    useEffect(() => {
        if (gameData) {
            fetch(`http://localhost:4044/getreviewbygameid?gameid=${gameData.gameId}`, {
                method: 'GET', mode: 'cors', headers: {
                    'Content-Type': 'application/json',
                },
            })
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error('Ошибка при получении данных с сервера');
                    }
                })
                .then((data) => {
                    const {id, text, rating, date} = data;
                    const reviewsData = [];
                    for (let i = 0; i < id.length; i++) {
                        const review = {
                            id: id[i], text: text[i], rating: rating[i], date: date[i],
                        };

                        reviewsData.push(review);
                    }
                    setReviews(reviewsData);
                })
                .catch((error) => {
                    console.error('Ошибка при получении данных с сервера', error);
                });
        }
    }, [gameData]);

    useEffect(() => {
        if (gameData) {
            fetch(`http://localhost:4044/getadminreviewbygameid?gameid=${gameData.gameId}`, {
                method: 'GET', mode: 'cors', headers: {
                    'Content-Type': 'application/json',
                },
            })
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error('Ошибка при получении данных с сервера');
                    }
                })
                .then((data) => {
                    const {
                        id,
                        summary,
                        gamePlay,
                        addictiveness,
                        stylization,
                        replayable,
                        gamePlayRating,
                        addictivenessRating,
                        stylizationRating,
                        replayableRating,
                        rating,
                        date,
                        adminName
                    } = data;
                    const adminReviewsData = [];
                    const adminReview1 = {
                        id: id,
                        summary: summary,
                        gamePlay: gamePlay,
                        addictiveness: addictiveness,
                        stylization: stylization,
                        replayable: replayable,
                        gamePlayRating: gamePlayRating,
                        addictivenessRating: addictivenessRating,
                        stylizationRating: stylizationRating,
                        replayableRating: replayableRating,
                        rating: rating,
                        date: date,
                        adminName: adminName
                    }
                    adminReviewsData.push(adminReview1);
                    setAdminReviews(adminReviewsData);
                })
                .catch((error) => {
                    console.error('Ошибка при получении данных с сервера', error);
                });
        }
    }, [gameData]);

    const getBackgroundColor = (rating) => {
        let backgroundColor = '';

        if (rating >= 0 && rating < 20) {
            backgroundColor = 'black';
        } else if (rating >= 20 && rating < 50) {
            backgroundColor = 'red';
        } else if (rating >= 50 && rating < 75) {
            backgroundColor = 'yellow';
        } else if (rating >= 75 && rating <= 100) {
            backgroundColor = 'green';
        }

        return backgroundColor;
    };


    if (!gameData || !reviews || !adminReviews) {
        return <p>Loading...</p>;
    }
    
    const {publisher, developer, year, description, gameId} = gameData;


    return (<div>

        <main>

            <Container maxWidth="xl" style={{marginBottom: '30px', width: '80%'}}>
                <h1 style={{textAlign: 'center'}}>
                    <span style={{fontSize: '60px'}}>{title}</span>
                    <span style={{fontSize: '20px'}}>({year})</span>
                </h1>
                <Paper elevation={3} style={{padding: '20px', margin: '0 auto'}}>
                    <p style={{fontSize: '23px'}}>{description}</p>
                    <p style={{textAlign: 'right', fontSize: '20px'}}>
                        Publisher: {publisher}, Developer: {developer}
                    </p>
                </Paper>
            </Container>


            <Container maxWidth="xl" style={{marginBottom: '40px', width: '80%'}}>
                <div style={{border: '1px solid #ccc', padding: '20px', backgroundColor: 'rgba(0, 0, 0, 0.04)'}}>
                    <Grid container spacing={3} justify="flex-end">
                        {adminReviews.map((adminReview) => (<Grid item key={adminReview.id} xs={12}>
                            <Typography variant="h6" style={{marginBottom: '10px'}}>
                                Admin Review (by {adminReview.adminName})
                                <span style={{float: 'right', marginTop: '-10px'}}>  
                                    <span
                                        style={{
                                            display: 'inline-block',
                                            padding: '6px 12px',
                                            border: '1px solid #000',
                                            borderRadius: '10px',
                                            backgroundColor: getBackgroundColor(adminReview.rating),
                                            color: '#fff',
                                            fontWeight: 'bold',
                                        }}
                                    >
                                        {adminReview.rating}
                                    </span>
                                </span>
                            </Typography>
                            <div style={{marginBottom: '20px'}}>
                                <hr style={{margin: '10px 0'}}/>
                                <Typography variant="body1">{adminReview.gamePlay}</Typography>
                                <Typography variant="h6" style={{color: '#1177d3'}}>
                                    Gameplay Rating: {adminReview.gamePlayRating}
                                </Typography>
                                <hr style={{margin: '10px 0'}}/>
                                <Typography variant="body1">{adminReview.addictiveness}</Typography>
                                <Typography variant="h6" style={{color: '#1177d3'}}>
                                    Addictiveness Rating: {adminReview.addictivenessRating}
                                </Typography>
                                <hr style={{margin: '10px 0'}}/>
                                <Typography variant="body1">{adminReview.stylization}</Typography>
                                <Typography variant="h6" style={{color: '#1177d3'}}>
                                    Stylization Rating: {adminReview.stylizationRating}
                                </Typography>
                                <hr style={{margin: '10px 0'}}/>
                                <Typography variant="body1">{adminReview.replayable}</Typography>
                                <Typography variant="h6" style={{color: '#1177d3'}}>
                                    Replayable Rating: {adminReview.replayableRating}
                                </Typography>
                                <hr style={{margin: '10px 0'}}/>
                                <Typography variant="body1"
                                            style={{marginBottom: '15px'}}>{adminReview.summary}</Typography>
                                <Typography align="right" variant="body2" component="div" sx={{fontSize: '16px'}}>
                                    Post date: {adminReview.date}
                                </Typography>
                            </div>
                        </Grid>))}
                    </Grid>
                </div>
            </Container>

            <Container maxWidth="xl" style={{marginBottom: '30px', width: '80%'}}>
                <p style={{textAlign: 'center', fontSize: '20px'}}>
                    Maybe this game is sold by our partners -
                    <a
                        href={`https://zaka-zaka.com/game/${title.replace(/: /g, '-').replace(/ /g, '-')}`}
                        style={{color: '#1177d3'}}
                    >
                        {title}
                    </a>
                </p>
            </Container>

            <Container sx={{py: 6}} maxWidth="xl" style={{width: '80%'}}>
                <Grid container spacing={10} justifyContent='center'>
                    {reviews.map((reviews) => (<Grid item key={reviews.id} xs={12} sm={6} md={6} lg={5}>
                        <Card
                            sx={{
                                height: '100%',
                                width: '100%',
                                display: 'flex',
                                flexDirection: 'column',
                                backgroundColor: 'rgba(0, 0, 0, 0.04)',
                                color: '#000000',
                            }}>
                            <Typography align="left" variant="h6" component="div"
                                        sx={{marginBottom: '8px', color: '#1177d3'}}>
                                Rating: {reviews.rating}
                            </Typography>
                            <Typography align="center" variant="h5" sx={{marginBottom: '8px'}}>
                                {reviews.text}
                            </Typography>
                            <Typography align="right" variant="body2" component="div" sx={{fontSize: '12px'}}>
                                Post date: {reviews.date}
                            </Typography>
                        </Card>
                    </Grid>))}
                </Grid>
            </Container>


            <Button
                component={Link}
                to={`/WriteReview/${title}?game=${encodeURIComponent(title)}&gameId=${gameId}`}
                size="small"
                fullWidth
                variant="contained"
            >
                Leave your personal review!
            </Button>

        </main>
    </div>);
}