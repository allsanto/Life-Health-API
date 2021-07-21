using Dapper;
using Life_Healthy_API.Data.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Life_Healthy_API.Data.Repository
{
    public class WeightRepository : RepositoryBase
    {
        public WeightRepository(IConfiguration configuration)
        {
            base.configuration = configuration;
        }

        public int InsertWeight_DAO(PesoEntity peso)
        {
            using var db = Connection;

            var query = @"INSERT INTO peso
                          (data_peso,
                           peso,
                           usuario_id)
                          VALUES(@DataPeso,
                                 @Peso,
                                 @UsuarioId);";

            return db.ExecuteScalar<int>(query, new
            {
                peso.DataPeso,
                peso.Peso,
                peso.UsuarioId
            });
        }

        public PesoEntity GetWeightById_DAO(int id)
        {
            using var db = Connection;

            var query = @"SELECT peso_id,
	                        data_peso,
	                        peso,
	                        status,
	                        usuario_id
	                        FROM peso
	                        WHERE peso_id = @id";

            return db.QueryFirstOrDefault<PesoEntity>(query, new { id });
        }

        public IEnumerable<PesoEntity> GetWeightByUserId_DAO(int id)
        {
            using var db = Connection;

            var query = @"SELECT peso_id,
	                        data_peso,
	                        peso,
	                        status,
	                        usuario_id
	                        FROM peso
	                        WHERE usuario_id = @id
                                AND status = 1";

            return db.Query<PesoEntity>(query, new { id });
        }

        public int DeleteWeight_DAO(int id)
        {
            using var db = Connection;

            var query = @"UPDATE peso
                            SET status = 2
                            WHERE peso_id = @id";

            return db.Execute(query, new { id });
        }

        public int UpdateWeight_DAO(DateTime DataPeso, decimal Peso, int UsuarioId, int PesoId)
        {
            using var db = Connection;

            var query = @"UPDATE peso 
                                SET data_peso = @DataPeso,
                                    peso = @Peso,
                                    usuario_id = @UsuarioId
                                WHERE peso_id = @PesoId";

            return db.Execute(query, new
            {
                DataPeso,
                Peso,
                UsuarioId,
                PesoId
            });
        }
    }
}
