using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Application;
using Domain;

namespace Infrastructure
{
    public class AlumnosDbContext
    {
        private readonly string _connectionString;

        public AlumnosDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Alumno> List()
        {
            var data = new List<Alumno>();

            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[Promedio],[Foto] FROM [Alumno]", con);
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Alumno
                    {
                        Id = (Guid)dr["Id"],
                        Nombre = (string)dr["Nombre"],
                        Edad = (int)dr["Edad"],
                        Promedio = (double)dr["Promedio"],
                        Foto = dr["Foto"] as string ?? FileConverterService.PlaceHolder
                    });
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Alumno Details(Guid id)
        {
            var data = new Alumno();

            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("SELECT [Id],[Nombre],[Edad],[Promedio],[Foto] FROM [Alumno] WHERE [Id] = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["Id"];
                    data.Nombre = (string)dr["Nombre"];
                    data.Edad = (int)dr["Edad"];
                    data.Promedio = (double)dr["Promedio"];
                    data.Foto = dr["Foto"] as string ?? FileConverterService.PlaceHolder;
                }
                return data;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Create(Alumno data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("INSERT INTO [Alumno] ([Id],[Nombre],[Edad],[Promedio],[Foto]) VALUES (@id,@nombre,@edad,@promedio,@foto)", con);
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
            cmd.Parameters.Add("edad", SqlDbType.Int).Value = data.Edad;
            cmd.Parameters.Add("promedio", SqlDbType.Float).Value = data.Promedio;
            cmd.Parameters.Add("foto", SqlDbType.NVarChar).Value = (object)data.Foto ?? DBNull.Value;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Edit(Alumno data)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("UPDATE [Alumno] SET [Nombre] = @nombre, [Edad] = @edad, [Promedio] = @promedio, [Foto] = @foto WHERE [Id] = @id", con);
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("nombre", SqlDbType.NVarChar, 128).Value = data.Nombre;
            cmd.Parameters.Add("edad", SqlDbType.Int).Value = data.Edad;
            cmd.Parameters.Add("promedio", SqlDbType.Float).Value = data.Promedio;
            cmd.Parameters.Add("foto", SqlDbType.NVarChar).Value = (object)data.Foto ?? DBNull.Value;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public void Delete(Guid id)
        {
            var con = new SqlConnection(_connectionString);
            var cmd = new SqlCommand("DELETE FROM [Alumno] WHERE [Id] = @id", con);
            cmd.Parameters.Add("id", SqlDbType.UniqueIdentifier).Value = id;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
