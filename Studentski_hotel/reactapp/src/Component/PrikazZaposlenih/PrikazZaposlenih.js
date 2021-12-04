import React, { useState, useEffect } from 'react'
import { withStyles, makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import './PrikazZaposlenih.css'
import axios from 'axios'
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';

const StyledTableCell = withStyles((theme) => ({
    head: {
        backgroundColor: theme.palette.common.black,
        color: theme.palette.common.white,
    },
    body: {
        fontSize: 14,
    },
}))(TableCell);

const StyledTableRow = withStyles((theme) => ({
    root: {
        '&:nth-of-type(odd)': {
            backgroundColor: theme.palette.action.hover,
        },
    },
   
}))(TableRow);


const useStyles = makeStyles({
    table: {
        width: '100%',
    }
   
});



const PrikazZaposlenih = () => {
    const [Data, setData] = useState({ Uposlenici: [] });
    const [DisplayedData, setDisplayedData] = useState({ Uposlenici: [] });

    const classes = useStyles();
    useEffect(() => {

        axios.get("https://localhost:44328/AdminApi/PrikazOsoblja").then(result => (
            setData({ Uposlenici: result.data.uposlenici }),
            setDisplayedData({ Uposlenici: result.data.uposlenici })

        ));


    }, []);

    const pretraga=(event)=>{
     const temp=Data.Uposlenici.filter(a=>{
        return a.ime.toLowerCase().startsWith(event.target.value.toLowerCase()) || a.prezime.toLowerCase().startsWith(event.target.value.toLowerCase());
    })
    setDisplayedData({Uposlenici:temp});
    }
    return (
        <div className='PrikazZaposlenih'>
            <div>
                <form noValidate autoComplete="off" >
                    <input type="text" placeholder="Search" onChange={pretraga}/>
                  </form>
                <TableContainer component={Paper}>
                    <Table className={classes.table} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <StyledTableCell>Ime</StyledTableCell>
                                <StyledTableCell align="right">Prezime</StyledTableCell>
                                <StyledTableCell align="right">Pozicija</StyledTableCell>
                                <StyledTableCell align="right">Datum Zaposlenja</StyledTableCell>
                                <StyledTableCell align="right"></StyledTableCell>

                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {DisplayedData.Uposlenici.map((row) => (
                                <StyledTableRow key={row.id}>
                                    <StyledTableCell component="th" scope="row">
                                        {row.ime}
                                    </StyledTableCell>
                                    <StyledTableCell align="right">{row.prezime}</StyledTableCell>
                                    <StyledTableCell align="right">{row.pozicija}</StyledTableCell>
                                    <StyledTableCell align="right">{row.datumZaposlenja}</StyledTableCell>
                                    <StyledTableCell align="right"><Button variant="outlined">Uredi</Button></StyledTableCell>

                                </StyledTableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </div>
        </div>
    );
}
export default PrikazZaposlenih;