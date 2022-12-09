import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import * as actions from "../actions/Patient";
import { Grid, Paper, TableContainer, Table, TableHead, TableRow, TableCell, TableBody, withStyles} from "@material-ui/core";
import PatientForm from "./MyForm";

const styles = theme => ({
    root: {
        "& .MuiTableCell-head": {
            fontSize: "1.25rem"
        }
    },
    paper: {
        margin: theme.spacing(2),
        padding: theme.spacing(2)
    }
})

const Patients = ({ classes, ...props }) => {
    const [currentId, setCurrentId] = useState(0)

    useEffect(() => {
        props.fetchAllPatients()
    }, [])
    
    return (
        <Paper className={classes.paper} elevation={3}>
            <Grid container>
                <Grid item xs={6}>
                    <PatientForm {...({ currentId, setCurrentId })} />
                </Grid>
                <Grid item xs={6}>
                    <TableContainer>
                        <Table>
                            <TableHead className={classes.root}>
                                <TableRow>
                                    <TableCell>Name</TableCell>
                                    <TableCell>Surname</TableCell>
                                    <TableCell>Date of birth</TableCell>
                                    <TableCell>Disease start</TableCell>
                                    <TableCell>Disease end</TableCell>
                                    <TableCell></TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {
                                    props.PatientList.map((record, index) => {
                                        return (<TableRow key={index} hover>
                                            <TableCell>{record.name}</TableCell>
                                            <TableCell>{record.surname}</TableCell>
                                            <TableCell>{record.dateOfBirth}</TableCell>
                                            <TableCell>{record.start}</TableCell>
                                            <TableCell>{record.end}</TableCell>
                                        </TableRow>)
                                    })
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
            </Grid>
        </Paper>
    );
}

const mapStateToProps = state => ({
    PatientList: state.Patient.list
})

const mapActionToProps = {
    fetchAllPatients: actions.fetchAll
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(Patients));