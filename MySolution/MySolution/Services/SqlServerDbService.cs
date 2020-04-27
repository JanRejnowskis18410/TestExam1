using MySolution.DTOs.Requests;
using MySolution.DTOs.Responses;
using MySolution.Exceptions;
using MySolution.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MySolution.Services
{
    public class SqlServerDbService : IDbService
    {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s18410;Integrated Security=True";

        public int CreateAnimal(Animal animal)
        {
            int result;
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var transaction = con.BeginTransaction();
                com.Transaction = transaction;
                SqlDataReader dr;

                try
                {
                    if (animal.Procedures != null)
                    {
                        foreach (CreateAnimalRequestProcedure procedure in animal.Procedures)
                        {
                            com.CommandText = "SELECT * FROM [Procedure] WHERE IdProcedure = @idProcedure";
                            if (!com.Parameters.Contains("idProcedure"))
                                com.Parameters.AddWithValue("idProcedure", procedure.IdProcedure);
                            else
                                com.Parameters["idProcedure"].Value = procedure.IdProcedure;
                            dr = com.ExecuteReader();
                            if (!dr.HasRows)
                            {
                                dr.Close();
                                transaction.Rollback();
                                throw new CreateAnimalWrongProcedureIdException("Procedura o id: " + procedure.IdProcedure + " nie istnieje w bazie danych!");
                            }
                            dr.Close();
                        }
                    }
                    com.CommandText = "SELECT * FROM Owner WHERE IdOwner = @idOwner";
                    com.Parameters.AddWithValue("idOwner", animal.IdOwner);
                    dr = com.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        dr.Close();
                        transaction.Rollback();
                        throw new CreateAnimalWrongOwnerIdException("Właściciel o id: " + animal.IdOwner + " nie istnieje w bazie danych!");
                    }
                    dr.Close();

                    com.CommandText = "INSERT INTO Animal VALUES (@name, @type, @admissionDate, @idOwner)";
                    com.Parameters.AddWithValue("name", animal.Name);
                    com.Parameters.AddWithValue("type", animal.Type);
                    com.Parameters.AddWithValue("admissionDate", animal.AdmissionDate);
                    com.ExecuteNonQuery();

                    com.CommandText = "SELECT MAX(IdAnimal) 'IdAnimal' FROM Animal";
                    dr = com.ExecuteReader();
                    dr.Read();
                    var newAnimalId = Int32.Parse(dr["IdAnimal"].ToString());
                    dr.Close();

                    if (animal.Procedures != null)
                    {
                        com.Parameters.AddWithValue("newAnimalId", newAnimalId);
                        foreach (CreateAnimalRequestProcedure procedure in animal.Procedures)
                        {
                            com.CommandText = "INSERT INTO Procedure_Animal VALUES (@idProcedure, @newAnimalId, CONVERT(date, GETDATE()))";
                            if (!com.Parameters.Contains("idProcedure"))
                                com.Parameters.AddWithValue("idProcedure", procedure.IdProcedure);
                            else
                                com.Parameters["idProcedure"].Value = procedure.IdProcedure;
                            com.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    result = newAnimalId;
                }
                catch (SqlException exc)
                {
                    transaction.Rollback();
                    throw new SqlServerException(exc.Message);
                }
            }
            return result;
        }

        public List<GetAnimalsResponse> getAnimals()
        {
            return getAnimals(null);
        }

        public List<GetAnimalsResponse> getAnimals(string[] parameters)
        {
            SqlDataReader dr;
            var list = new List<GetAnimalsResponse>();
            try
            {
                using (var con = new SqlConnection(ConString))
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    con.Open();
                    if (parameters == null)
                    {
                        com.CommandText = "SELECT Name, Type, AdmissionDate, LastName FROM Animal JOIN Owner ON Animal.IdOwner = Owner.IdOwner ORDER BY AdmissionDate DESC;";
                    }
                    else
                    {
                        com.CommandText = "SELECT Name, Type, AdmissionDate, LastName FROM Animal JOIN Owner ON Animal.IdOwner = Owner.IdOwner ORDER BY " + parameters[0] + " " + parameters[1] + ";";
                    }
                    dr = com.ExecuteReader();
                    while (dr.Read())
                    {
                        var animal = new GetAnimalsResponse();
                        animal.Name = dr["Name"].ToString();
                        animal.AnimalType = dr["Type"].ToString();
                        animal.DateOfAdmission = DateTime.Parse(dr["AdmissionDate"].ToString());
                        animal.LastNameOfOwner = dr["LastName"].ToString();
                        list.Add(animal);
                    }
                    return list;
                }
            }
            catch (SqlException exc)
            {
                throw new SqlServerException(exc.Message);
            }
        }
    }
}
