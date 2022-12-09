import React, {  useEffect } from "react";
import { Grid, TextField, withStyles, Button } from "@material-ui/core";
import useForm from "./useForm";
import { connect } from "react-redux";
import * as actions from "../actions/Patient";

const styles = theme => ({
    root: {
        '& .MuiTextField-root': {
            margin: theme.spacing(1),
            minWidth: 230,
        }
    },
    formControl: {
        margin: theme.spacing(1),
        minWidth: 230,
    },
    smMargin: {
        margin: theme.spacing(1)
    }
})

const PatientForm = ({ classes, ...props }) => {

    const {
        values,
        setValues,
        errors,
        setErrors,
        handleInputChange,
        resetForm
    } = useForm(props.setCurrentId)

    const handleSubmit = e => {
        e.preventDefault()
        const onSuccess = () => {
            resetForm()
        }
        if (props.currentId == 0)
            props.createPatient(values, onSuccess)
        else
            props.updatePatient(props.currentId, values, onSuccess)
        
    }

    useEffect(() => {
        if (props.currentId != 0) {
            setValues({
                ...props.PatientList.find(x => x.id == props.currentId)
            })
            setErrors({})
        }
    }, [props.currentId])

    return (
        <form autoComplete="off" noValidate className={classes.root} onSubmit={handleSubmit}>
            <Grid container>
                <Grid item xs={6}>
                    <TextField
                        name="name"
                        variant="outlined"
                        label="Name"
                        onChange={handleInputChange}
                        {...(errors.name && { error: true, helperText: errors.name })}
                    />
                    <TextField
                        name="surname"
                        variant="outlined"
                        label="Surname"
                        onChange={handleInputChange}
                        {...(errors.email && { error: true, helperText: errors.email })}
                    />
                </Grid>
                <Grid item xs={6}>
                    <TextField
                        name="birthdate"
                        variant="outlined"
                        label="Birth Date"
                        onChange={handleInputChange}
                    />
                    <TextField
                        name="start"
                        variant="outlined"
                        label="Disease Start Date"
                        onChange={handleInputChange}
                    />
                    <TextField
                        name="end"
                        variant="outlined"
                        label="Disease End Date"
                        onChange={handleInputChange}
                    />
                    <div>
                        <Button
                            variant="contained"
                            color="primary"
                            type="submit"
                            className={classes.smMargin}
                        >
                            Submit
                        </Button>
                    </div>
                </Grid>
            </Grid>
        </form>
    );
}


const mapStateToProps = state => ({
    PatientList: state.Patient.list
})

const mapActionToProps = {
    createPatient: actions.create,
    updatePatient: actions.update
}

export default connect(mapStateToProps, mapActionToProps)(withStyles(styles)(PatientForm));