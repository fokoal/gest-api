using System.Text;
using gesteco.api.OutputModels;

namespace gesteco.api.Services {
    public static class GeneratePDF {



        public static string GetHtmlString(EmailDTO emailDTO)
        {
            var sb = new StringBuilder();
			sb.Append(@"
            <html>
            <head><link rel = stylesheet href=https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap /> 
             <link rel = stylesheet href = https://fonts.googleapis.com/icon?family=Material+Icons />
             <link rel = stylesheet href = https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css integrity=sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T crossorigin=anonymous>
			</head>
            <body> <div class='row' id='html-ecofacture'>  "
			+ "<div class='col-lg-8 offset-lg-2'> <img src='"+emailDTO.Origin+"/logo-laval.png' alt='Ville de Laval'>"
			+ "<div class='form-group col-sm-12'>"
			+ "<label class=' col-md-4 m-l-1'>"
			+ "<div>Ville de Laval</div>"
			+ "<div> Service des Finance</div>"
			+ "<div> 1333 Boul.Chomedey</div>"
			+ "<div> Bureau 601</div>"
			+ "<div>Laval, QC, H7V 3Z4</div>"
			+ "</label>"
			+ "<label class='col-md-4 m-l-1 text-right'>No Reçu :" + emailDTO.VisiteDTO.NumeroFacture +" </label>"
			+ "</div>"
			+ "<div class='form-group col-sm-12'>"
			+ "<label class='col-md-2 m-l-1 text-right'> </label>"
			+ "<label class='col-md-2 m-l-1'> </label>"
			+ "<label class='col-md-2 m-l-1'></label>"
			+ "<label class='col-md-2 m-l-1'> </label>"
			+ "<label class='col-md-4 m-l-1 text-right'>Site " + emailDTO.VisiteDTO. Ecocentre.Nom + "</label>"
			+ "</div>"
			+ "<div class='form-group col-sm-12'>"
			+ "<label class='col-md-2 m-l-1'></label>"
			+ "<label class='col-md-2 m-l-1'></label>"
			+ "<label class='col-md-2 m-l-1'></label>"
			+ "<label class='col-md-2 m-l-1'>"
			+ "<label class='col-md-4 m-l-1 text-right'> Date "+ emailDTO.VisiteDTO.DateVisite+ "</label>"
			+ "</label></div>"
			+ "<div class='clearfix col-sm-12'></div>"
			+ "<div class='form-group col-sm-12'>"
			+ "<label class=' col-md-6 m-l-1'>"
			+ "<div>" + emailDTO.VisiteDTO.Client.Nom +"  "+emailDTO.VisiteDTO.Client.Prenom +"</div>"
			+ "<div>"+ emailDTO.VisiteDTO.Client.Telephone +"</div>"
			+ "<div>"+ emailDTO.VisiteDTO. Provenance.Adresse +"</div>"
			+ "</label></div><div class='table-responsive'><div>"
			+ "<table class='table table-striped'>"
								+ "<thead>"
								+ "<tr>"
								+ "<th scope='col'>Materiaux</th>"
								+ "<th scope='col'>Mesure.</th>"
								+ "<th scope='col'>Volume</th>"
								+ "<th scope='col'>Prix($CAD)</th>"
								+ "</tr>"
								+ "</thead><tbody ><tr><td>");


            foreach (var m in emailDTO.VisiteDTO.Matieres)
            {
				sb.AppendFormat(@"<div>"+ m .Description+ " </div>");
            }

			sb.Append(@"</td><td> <di>Long="+ emailDTO.VisiteDTO.Transaction.Longueur+ "</di><di>Larg="+ emailDTO.VisiteDTO.Transaction.Largeur + "</di><di>Haut="+ emailDTO.VisiteDTO.Transaction. Hauteur +"</di></td>" +
			"<td>"+ emailDTO.VisiteDTO.Transaction.Volume +"</td>" +
			"<td>" + emailDTO.Prix + "</td></tr>" +
			"<tr> <td rowspan='3'></td>" +
			"<td colspan='2'>Avoir(m3)</td>" +
			"<td>"+ emailDTO.VisiteDTO.Transaction.Quantite_Utilisee +"</td></tr>" +
			"<tr> <td colspan='2'>Total</td>" +
			"<td >"+ emailDTO.VisiteDTO.Transaction.Total +"</td></tr>" +
			"<tr>  <td colspan='2'>Solde avoir restant(m3)</td>" +
			"<td >"+ emailDTO.VisiteDTO.Provenance.Quantite_Disponible 
			+"</td></tr></tbody></table></div></div></div></div></body></html>");

            return sb.ToString();
        }
		 


    }

}
