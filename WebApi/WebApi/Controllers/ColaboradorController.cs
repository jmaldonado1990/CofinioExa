using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ColaboradorController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ColaboradorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("ListarColaboradores")]
        public List<ColaboradorModel> ListarColaboradores()
        {
            return ListarColaboradoresDB();
        }


        private List<ColaboradorModel> ListarColaboradoresDB()
        {

            List<ColaboradorModel> _lstColaborador = new();

            string myCadena = _configuration.GetConnectionString("MySQL");

            using (MySqlConnection _con = new(myCadena))
                try
                {
                    using MySqlCommand _cmd = new("SELECT IDCOLABORADOR, NOMBRE, APELLIDO, DIRECCION, EDAD, PROFESION, ESTADOCIVIL, CASE WHEN CAST(EDAD AS DECIMAL) >= 18 AND CAST(EDAD AS DECIMAL) <= 25 THEN 'FUERA DE PELIGRO' WHEN CAST(EDAD AS DECIMAL) >= 26 AND CAST(EDAD AS DECIMAL) <= 50 THEN 'TENGA CUIDADO, TOME TODAS LAS MEDIDAS DE PREVENSION' WHEN CAST(EDAD AS DECIMAL) > 50 THEN 'POR FAVOR QUEDARSE EN CASA' ELSE 'NO TIENE MENSAJE' END 'MENSAJE' FROM COLABORADOR", _con);
                    _cmd.CommandType = CommandType.Text;

                    if (_con.State != ConnectionState.Open)
                    {
                        _con.Open();
                    }

                    using MySqlDataReader _reader = _cmd.ExecuteReader();
                    while (_reader.Read())
                    {
                        _lstColaborador.Add(new ColaboradorModel
                        {
                            IDCOLABORADOR = Convert.ToInt16(_reader["IDCOLABORADOR"]),
                            NOMBRE = _reader["NOMBRE"].ToString(),
                            APELLIDO = _reader["APELLIDO"].ToString(),
                            DIRECCION = _reader["DIRECCION"].ToString(),
                            EDAD = _reader["EDAD"].ToString(),
                            PROFESION = _reader["PROFESION"].ToString(),
                            ESTADOCIVIL = _reader["ESTADOCIVIL"].ToString(),
                            MENSAJE = _reader["MENSAJE"].ToString()

                        });
                    }

                }
                catch (Exception)
                {

                }
                finally
                {
                    _con.Close();
                }

            return _lstColaborador;

        }


    }
}
