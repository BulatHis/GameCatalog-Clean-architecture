import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import CssBaseline from '@mui/material/CssBaseline';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {Link} from 'react-router-dom';
import {createTheme, ThemeProvider} from '@mui/material/styles';
import React, {useEffect, useState} from 'react';


const defaultTheme = createTheme();

export default function Games() {
    const [cards, setCards] = useState([]);

    useEffect(() => {
        fetch('http://localhost:4044/getalltitles')
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    throw new Error('Ошибка при получении данных с сервера');
                }
            })
            .then((data) => {
                const {titles, images} = data;
                const gameCards = titles.map((title, index) => ({
                    id: index,
                    title: title,
                    image: images[index]
                }));
                setCards(gameCards);
            })
            .catch((error) => {
                console.error('Ошибка при получении данных с сервера', error);
            });
    }, []);


    return (
        <ThemeProvider theme={defaultTheme}>
            <CssBaseline/>
            <main>
                <Container sx={{py: 8}} maxWidth="xl">
                    <Grid container spacing={4} justifyContent="space-between">
                        {cards.map((card) => (
                            <Grid item key={card.id} xs={12} sm={6} md={4}>
                                <Card sx={{height: '100%', display: 'flex', flexDirection: 'column'}}>
                                    <Typography align="center" gutterBottom variant="h5" component="h2"
                                    >
                                        {card.title}
                                    </Typography>
                                    <img
                                        src={`http://localhost:4044/GameImg/${card.image}`}
                                        alt={card.title}
                                        style={{
                                            objectFit: 'cover',
                                            objectPosition: 'center',
                                            flexGrow: 1,
                                        }}
                                    />
                                    <CardActions>
                                        <Button
                                            component={Link}
                                            to={`/game/${card.title}`}
                                            size="medium"
                                            fullWidth
                                            variant="contained"
                                            sx={{mt: 2, mb: 2}}
                                        >
                                            View Game
                                        </Button>
                                    </CardActions>
                                </Card>
                            </Grid>
                        ))}
                    </Grid>
                </Container>
            </main>
            {/* Footer */}
            <Box sx={{bgcolor: 'background.paper', p: 6}} component="footer">
                <Typography variant="h6" align="center" gutterBottom></Typography>
                <Typography variant="subtitle1" align="center" color="text.secondary" component="p"></Typography>
            </Box>
        </ThemeProvider>
    );
}